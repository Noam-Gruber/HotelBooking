using System;
using Common;
using System.Net;
using System.Linq;
using Server.Data;
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
        private static readonly int _port = Params.GetPort();

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
                    string handshakeCmd = IOstring.ReadString(stream);
                    if (handshakeCmd != "getpublickey")
                        throw new Exception("Expected handshake getpublickey!");

                    string publicKey = rsa.ToXmlString(false); // public only
                    IOstring.WriteString(stream, publicKey);

                    // Step 2: Receive encrypted AES key (base64), and decrypt it
                    string encryptedAesKeyB64 = IOstring.ReadString(stream);
                    byte[] encryptedAesKey = Convert.FromBase64String(encryptedAesKeyB64);
                    byte[] aesKey = rsa.Decrypt(encryptedAesKey, false);

                    // Step 3: Receive IV (base64)
                    string ivB64 = IOstring.ReadString(stream);
                    byte[] aesIV = Convert.FromBase64String(ivB64);

                    // Now ready to receive/send everything via AES
                    while (true)
                    {
                        // Step 4: Receive encrypted request (base64)
                        string encReqB64 = IOstring.ReadString(stream);
                        byte[] encReq = Convert.FromBase64String(encReqB64);
                        string reqJson = Encription.DecryptAes(encReq, aesKey, aesIV);

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

                                case "sendchat":
                                    req.ChatMessage.Timestamp = DateTime.Now;
                                    db.ChatMessages.Add(req.ChatMessage);
                                    db.SaveChanges();
                                    result = req.ChatMessage;
                                    statusCode = 201;
                                    break;

                                case "getchat":
                                    var messages = db.ChatMessages
                                        .Where(m => m.SessionId == req.SessionId)
                                        .OrderBy(m => m.Timestamp)
                                        .ToList();
                                    result = messages;
                                    statusCode = 0;
                                    break;

                                case "getallchats":
                                    // לאדמין - כל ההודעות
                                    var allMessages = db.ChatMessages
                                        .OrderBy(m => m.Timestamp)
                                        .ToList();
                                    result = allMessages;
                                    statusCode = 0;
                                    break;

                                case "deleteoldchats":
                                    int minutes = req.Minutes;
                                    DateTime cutoff = DateTime.Now.AddMinutes(-minutes);
                                    var oldSessions = db.ChatMessages
                                        .GroupBy(m => m.SessionId)
                                        .Where(g => g.Max(m => m.Timestamp) < cutoff)
                                        .Select(g => g.Key)
                                        .ToList();
                                    var messagesToDelete = db.ChatMessages.Where(m => oldSessions.Contains(m.SessionId));
                                    db.ChatMessages.RemoveRange(messagesToDelete);
                                    db.SaveChanges();
                                    statusCode = 0;
                                    result = "Deleted old chats";
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
                        byte[] encResp = Encription.EncryptAes(respJson, aesKey, aesIV);
                        IOstring.WriteString(stream, Convert.ToBase64String(encResp));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
        }
    }
}
