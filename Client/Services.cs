using Common;
using System;
using System.Text;
using Common.Entities;
using Common.Messages;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// Provides methods to interact with the booking server API over TCP.
    /// </summary>
    internal class BookingApi
    {
        private readonly int _port = Params.GetPort();
        private readonly string _host = Params.GetServerAddress();

        /// <summary>
        /// Establishes a TCP connection to the booking server.
        /// </summary>
        /// <returns>A connected <see cref="TcpClient"/> instance.</returns>
        private TcpClient Connect()
        {
            var client = new TcpClient();
            client.Connect(_host, _port);
            return client;
        }

        /// <summary>
        /// Serializes and sends a request object to the server via the specified stream.
        /// </summary>
        /// <param name="stream">The network stream to write to.</param>
        /// <param name="req">The request object to send.</param>
        private void WriteRequest(NetworkStream stream, object req)
        {
            var json = JsonConvert.SerializeObject(req);
            var payload = Encoding.UTF8.GetBytes(json);
            var length = BitConverter.GetBytes(payload.Length);
            stream.Write(length, 0, 4);
            stream.Write(payload, 0, payload.Length);
        }

        /// <summary>
        /// Reads a JSON response from the server via the specified stream.
        /// </summary>
        /// <param name="stream">The network stream to read from.</param>
        /// <returns>The response JSON string.</returns>
        private string ReadResponseJson(NetworkStream stream)
        {
            var lenBuf = new byte[4];
            FillBuffer(stream, lenBuf);
            int respLen = BitConverter.ToInt32(lenBuf, 0);

            var respBuf = new byte[respLen];
            FillBuffer(stream, respBuf);

            return Encoding.UTF8.GetString(respBuf);
        }

        /// <summary>
        /// Reads the specified number of bytes from the stream into the buffer.
        /// </summary>
        /// <param name="stream">The network stream to read from.</param>
        /// <param name="buffer">The buffer to fill.</param>
        /// <exception cref="Exception">Thrown if the connection is closed by the server.</exception>
        private void FillBuffer(NetworkStream stream, byte[] buffer)
        {
            int read = 0;
            while (read < buffer.Length)
            {
                int n = stream.Read(buffer, read, buffer.Length - read);
                if (n == 0) throw new Exception("Connection closed by server");
                read += n;
            }
        }

        /// <summary>
        /// Retrieves all bookings from the server.
        /// </summary>
        /// <returns>A list of <see cref="Booking"/> objects.</returns>
        /// <exception cref="Exception">Thrown if the server returns an error status.</exception>
        public List<Booking> GetAll()
        {
            using var client = Connect();
            using var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "getall" });
            var respJson = ReadResponseJson(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<List<Booking>>>(respJson);
            if (resp.Status != 0)
                throw new Exception("Server error: " + resp.Data);
            return resp.Data ?? new List<Booking>();
        }

        /// <summary>
        /// Retrieves a booking by its identifier.
        /// </summary>
        /// <param name="id">The booking identifier.</param>
        /// <returns>The <see cref="Booking"/> if found; otherwise, null.</returns>
        /// <exception cref="Exception">Thrown if the server returns an error status.</exception>
        public Booking? Get(int id)
        {
            using var client = Connect();
            using var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "get", Id = id });
            var respJson = ReadResponseJson(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);
            if (resp.Status != 0)
                throw new Exception("Server error: " + resp.Data);
            return resp.Data;
        }

        /// <summary>
        /// Creates a new booking on the server.
        /// </summary>
        /// <param name="b">The <see cref="Booking"/> to create.</param>
        /// <exception cref="Exception">Thrown if the server does not return a 201 status.</exception>
        public void Create(Booking b)
        {
            using var client = Connect();
            using var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "create", Booking = b });
            var respJson = ReadResponseJson(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);
            if (resp.Status != 201)
                throw new Exception("Create failed: " + resp.Data);
        }

        /// <summary>
        /// Updates an existing booking on the server.
        /// </summary>
        /// <param name="b">The <see cref="Booking"/> to update.</param>
        /// <exception cref="Exception">Thrown if the server does not return a 200 status.</exception>
        public void Update(Booking b)
        {
            using var client = Connect();
            using var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "update", Booking = b });
            var respJson = ReadResponseJson(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);
            if (resp.Status != 200)
                throw new Exception("Update failed: " + resp.Data);
        }

        /// <summary>
        /// Deletes a booking by its identifier.
        /// </summary>
        /// <param name="id">The booking identifier.</param>
        /// <exception cref="Exception">Thrown if the server does not return a 204 status.</exception>
        public void Delete(int id)
        {
            using var client = Connect();
            using var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "delete", Id = id });
            var respJson = ReadResponseJson(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<string>>(respJson);
            if (resp.Status != 204)
                throw new Exception("Delete failed: " + resp.Data);
        }
    }
}
