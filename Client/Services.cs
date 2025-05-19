using Common;
using System;
using System.IO;
using System.Text;
using Common.Entities;
using Common.Messages;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Client
{
    /// <summary>
    /// Provides client-side services for communicating with the hotel booking server,
    /// including secure connection establishment, booking CRUD operations, and encryption utilities.
    /// </summary>
    public class Services : IDisposable
    {
        /// <summary>
        /// The server host address.
        /// </summary>
        private readonly string _host = Params.GetServerAddress();

        /// <summary>
        /// The server port number.
        /// </summary>
        private readonly int _port = Params.GetPort();

        /// <summary>
        /// The TCP client used for communication.
        /// </summary>
        private TcpClient client;

        /// <summary>
        /// The network stream for data transmission.
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// The AES encryption key.
        /// </summary>
        private byte[] aesKey;

        /// <summary>
        /// The AES initialization vector.
        /// </summary>
        private byte[] aesIV;

        /// <summary>
        /// Initializes a new instance of the <see cref="Services"/> class and establishes a secure connection.
        /// </summary>
        public Services()
        {
            SecureConnect();
        }

        /// <summary>
        /// Establishes a secure connection to the server using RSA for key exchange and AES for encryption.
        /// </summary>
        private void SecureConnect()
        {
            client = new TcpClient(_host, _port);
            stream = client.GetStream();

            // 1. Handshake: get public key
            WriteString("getpublickey");
            string publicKeyXml = ReadString();

            // 2. Generate AES key and IV
            using var aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();
            aesKey = aes.Key;
            aesIV = aes.IV;

            // 3. Send encrypted AES key
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKeyXml);
            byte[] encryptedAesKey = rsa.Encrypt(aesKey, false);
            WriteString(Convert.ToBase64String(encryptedAesKey));

            // 4. Send IV
            WriteString(Convert.ToBase64String(aesIV));
        }

        // ------- API Functions: ---------

        /// <summary>
        /// Sends a request to create a new booking on the server.
        /// </summary>
        /// <param name="booking">The booking to create.</param>
        /// <exception cref="Exception">Thrown if the server returns an error status.</exception>
        public void CreateBooking(Booking booking)
        {
            var req = new RequestMessage { Command = "create", Booking = booking };
            WriteEncrypted(req);

            var respJson = ReadEncrypted();
            var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);

            if (resp.Status != 201)
                throw new Exception("Server error: " + resp.Data);
        }

        /// <summary>
        /// Retrieves all bookings from the server.
        /// </summary>
        /// <returns>A list of all bookings.</returns>
        /// <exception cref="Exception">Thrown if the server returns an error status.</exception>
        public List<Booking> GetAll()
        {
            WriteEncrypted(new RequestMessage { Command = "getall" });
            string respJson = ReadEncrypted();
            var resp = JsonConvert.DeserializeObject<ResponseMessage<List<Booking>>>(respJson);
            if (resp.Status != 0)
                throw new Exception("Server error: " + resp.Data);
            return resp.Data ?? new List<Booking>();
        }

        /// <summary>
        /// Updates an existing booking on the server.
        /// </summary>
        /// <param name="b">The booking to update.</param>
        /// <exception cref="Exception">Thrown if the update fails.</exception>
        public void Update(Booking b)
        {
            WriteEncrypted(new RequestMessage { Command = "update", Booking = b });
            string respJson = ReadEncrypted();
            var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);
            if (resp.Status != 200)
                throw new Exception("Update failed: " + resp.Data);
        }

        /// <summary>
        /// Deletes a booking from the server by its identifier.
        /// </summary>
        /// <param name="id">The booking identifier.</param>
        /// <exception cref="Exception">Thrown if the delete operation fails.</exception>
        public void Delete(int id)
        {
            WriteEncrypted(new RequestMessage { Command = "delete", Id = id });
            string respJson = ReadEncrypted();
            var resp = JsonConvert.DeserializeObject<ResponseMessage<string>>(respJson);
            if (resp.Status != 204)
                throw new Exception("Delete failed: " + resp.Data);
        }

        // ------ Encryption and Decryption --------

        /// <summary>
        /// Serializes and encrypts a request object, then sends it to the server.
        /// </summary>
        /// <param name="req">The request object to send.</param>
        private void WriteEncrypted(object req)
        {
            string json = JsonConvert.SerializeObject(req);
            byte[] enc = EncryptAes(json, aesKey, aesIV);
            WriteString(Convert.ToBase64String(enc));
        }

        /// <summary>
        /// Reads and decrypts an encrypted response from the server.
        /// </summary>
        /// <returns>The decrypted response string.</returns>
        private string ReadEncrypted()
        {
            string encB64 = ReadString();
            byte[] enc = Convert.FromBase64String(encB64);
            return DecryptAes(enc, aesKey, aesIV);
        }

        /// <summary>
        /// Writes a UTF-8 encoded string to the network stream, prefixed with its length.
        /// </summary>
        /// <param name="str">The string to write.</param>
        private void WriteString(string str)
        {
            var buf = Encoding.UTF8.GetBytes(str);
            var len = BitConverter.GetBytes(buf.Length);
            stream.Write(len, 0, 4);
            stream.Write(buf, 0, buf.Length);
        }

        /// <summary>
        /// Reads a UTF-8 encoded string from the network stream, using the prefixed length.
        /// </summary>
        /// <returns>The string read from the stream.</returns>
        private string ReadString()
        {
            var lenBuf = new byte[4];
            stream.Read(lenBuf, 0, 4);
            int len = BitConverter.ToInt32(lenBuf, 0);

            var buf = new byte[len];
            int read = 0;
            while (read < len) read += stream.Read(buf, read, len - read);
            return Encoding.UTF8.GetString(buf);
        }

        /// <summary>
        /// Encrypts a plain text string using AES encryption.
        /// </summary>
        /// <param name="plain">The plain text to encrypt.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>The encrypted data as a byte array.</returns>
        private byte[] EncryptAes(string plain, byte[] key, byte[] iv)
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
        private string DecryptAes(byte[] data, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key; aes.IV = iv;
            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(data);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Services"/> instance.
        /// </summary>
        public void Dispose()
        {
            stream?.Dispose();
            client?.Close();
        }
    }
}
