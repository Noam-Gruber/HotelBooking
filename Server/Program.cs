using System;
using System.IO;
using System.Net;
using System.Linq;
using Server.Data;
using System.Text;
using Common.Messages;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Server
{
    /// <summary>
    /// Entry point for the server application. Handles incoming TCP connections,
    /// performs secure handshake, and processes booking-related commands.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The port number the server listens on.
        /// </summary>
        private static readonly int _port = Common.Params.GetPort();

        /// <summary>
        /// The RSA key pair used for secure key exchange.
        /// </summary>
        private static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

        /// <summary>
        /// Main entry point. Starts the TCP server and listens for incoming client connections.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Loopback, _port);
            listener.Start();
            Console.WriteLine($"TCP server listening on port {_port}...");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(_ => HandleClient(client));
            }
        }

        /// <summary>
        /// Handles an individual client connection, performing handshake, decryption, and command processing.
        /// </summary>
        /// <param name="client">The connected TCP client.</param>
        static void HandleClient(TcpClient client)
        {
            try
            {
                using (client)
                using (var stream = client.GetStream())
                {
                    // Step 1: Handshake - send public key
                    string handshakeCmd = ReadString(stream);
                    if (handshakeCmd != "getpublickey")
                        throw new Exception("Expected handshake getpublickey!");

                    string publicKey = rsa.ToXmlString(false); // public only
                    WriteString(stream, publicKey);

                    // Step 2: Receive encrypted AES key (base64), and decrypt it
                    string encryptedAesKeyB64 = ReadString(stream);
                    byte[] encryptedAesKey = Convert.FromBase64String(encryptedAesKeyB64);
                    byte[] aesKey = rsa.Decrypt(encryptedAesKey, false);

                    // Step 3: Receive IV (base64)
                    string ivB64 = ReadString(stream);
                    byte[] aesIV = Convert.FromBase64String(ivB64);

                    // Now ready to receive/send everything via AES
                    while (true)
                    {
                        // Step 4: Receive encrypted request (base64)
                        string encReqB64 = ReadString(stream);
                        byte[] encReq = Convert.FromBase64String(encReqB64);
                        string reqJson = DecryptAes(encReq, aesKey, aesIV);

                        var req = JsonConvert.DeserializeObject<RequestMessage>(reqJson);

                        // Step 5: Process the request
                        object result = null;
                        int statusCode = 0;

                        using (var db = new ApplicationDbContext())
                        {
                            switch (req.Command?.ToLowerInvariant())
                            {
                                case "getall":
                                    result = db.Bookings.OrderBy(b => b.CheckInDate).ToList();
                                    statusCode = 0;
                                    break;
                                case "get":
                                    var item = db.Bookings.Find(req.Id);
                                    if (item == null) statusCode = 1;
                                    else result = item;
                                    break;
                                case "create":
                                    db.Bookings.Add(req.Booking);
                                    db.SaveChanges();
                                    result = req.Booking;
                                    statusCode = 201;
                                    break;
                                case "update":
                                    var updateItem = db.Bookings.Find(req.Booking.Id);
                                    if (updateItem != null)
                                    {
                                        updateItem.GuestName = req.Booking.GuestName;
                                        db.SaveChanges();
                                        statusCode = 200;
                                        result = updateItem;
                                    }
                                    else
                                    {
                                        statusCode = 1;
                                        result = "Not found for update";
                                    }
                                    break;
                                case "delete":
                                    var deleteItem = db.Bookings.Find(req.Id);
                                    if (deleteItem != null)
                                    {
                                        db.Bookings.Remove(deleteItem);
                                        db.SaveChanges();
                                        statusCode = 204;
                                        result = "Deleted";
                                    }
                                    else
                                    {
                                        statusCode = 1;
                                        result = "Not found for deletion";
                                    }
                                    break;
                                default:
                                    statusCode = 2;
                                    result = $"Unknown command '{req.Command}'";
                                    break;
                            }
                        }

                        // Step 6: Send encrypted response
                        var respObj = new { Status = statusCode, Data = result };
                        string respJson = JsonConvert.SerializeObject(respObj);
                        byte[] encResp = EncryptAes(respJson, aesKey, aesIV);
                        WriteString(stream, Convert.ToBase64String(encResp));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
        }

        // ===================== Tools =====================

        /// <summary>
        /// Writes a UTF-8 encoded string to the network stream, prefixed with its length.
        /// </summary>
        /// <param name="s">The network stream.</param>
        /// <param name="str">The string to write.</param>
        static void WriteString(NetworkStream s, string str)
        {
            var buf = Encoding.UTF8.GetBytes(str);
            var len = BitConverter.GetBytes(buf.Length);
            s.Write(len, 0, 4);
            s.Write(buf, 0, buf.Length);
        }

        /// <summary>
        /// Reads a UTF-8 encoded string from the network stream, using the prefixed length.
        /// </summary>
        /// <param name="s">The network stream.</param>
        /// <returns>The string read from the stream.</returns>
        static string ReadString(NetworkStream s)
        {
            var lenBuf = new byte[4];
            s.Read(lenBuf, 0, 4);
            int len = BitConverter.ToInt32(lenBuf, 0);

            var buf = new byte[len];
            int read = 0;
            while (read < len) read += s.Read(buf, read, len - read);
            return Encoding.UTF8.GetString(buf);
        }

        /// <summary>
        /// Encrypts a plain text string using AES encryption.
        /// </summary>
        /// <param name="plain">The plain text to encrypt.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>The encrypted data as a byte array.</returns>
        static byte[] EncryptAes(string plain, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key; aes.IV = iv;
            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plain);
            sw.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Decrypts AES-encrypted data to a plain text string.
        /// </summary>
        /// <param name="data">The encrypted data.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>The decrypted plain text string.</returns>
        static string DecryptAes(byte[] data, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key; aes.IV = iv;
            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(data);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
