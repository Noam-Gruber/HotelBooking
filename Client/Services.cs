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
    internal class BookingApi
    {
        private readonly int _port = Params.GetPort();
        private readonly string _host = Params.GetServerAddress();

        private TcpClient Connect()
        {
            var client = new TcpClient();
            client.Connect(_host, _port);
            return client;
        }

        private void WriteRequest(NetworkStream stream, object req)
        {
            var json = JsonConvert.SerializeObject(req);
            var payload = Encoding.UTF8.GetBytes(json);
            var length = BitConverter.GetBytes(payload.Length);
            stream.Write(length, 0, 4);
            stream.Write(payload, 0, payload.Length);
        }

        private string ReadResponseJson(NetworkStream stream)
        {
            var lenBuf = new byte[4];
            FillBuffer(stream, lenBuf);
            int respLen = BitConverter.ToInt32(lenBuf, 0);

            var respBuf = new byte[respLen];
            FillBuffer(stream, respBuf);

            return Encoding.UTF8.GetString(respBuf);
        }

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
