using Common;
using Common.Entities;
using Common.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class BookingApi
    {
        private const int PORT = 9000;
        private readonly string _host = Params.GetServerAddress();

        private TcpClient Connect()
        {
            var client = new TcpClient();
            client.Connect(_host, PORT);
            return client;
        }

        private void WriteRequest(NetworkStream stream, RequestMessage req)
        {
            var reqJson = JsonConvert.SerializeObject(req);
            var reqBytes = Encoding.UTF8.GetBytes(reqJson);
            var reqLen = BitConverter.GetBytes(reqBytes.Length);

            stream.Write(reqLen, 0, 4);
            stream.Write(reqBytes, 0, reqBytes.Length);
        }

        private string ReadResponse(NetworkStream stream)
        {
            var lenBuf = new byte[4];
            stream.Read(lenBuf, 0, 4);
            int respLen = BitConverter.ToInt32(lenBuf, 0);

            var respBuf = new byte[respLen];
            int read = 0;
            while (read < respLen)
                read += stream.Read(respBuf, read, respLen - read);

            return Encoding.UTF8.GetString(respBuf);
        }

        public List<Booking> GetAll()
        {
            using var client = Connect();
            var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "getall" });
            var respJson = ReadResponse(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<List<Booking>>>(respJson);
            return resp!.Data!;
        }

        public Booking? Get(int id)
        {
            using var client = Connect();
            var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage { Command = "get", Id = id });
            var respJson = ReadResponse(stream);
            var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);
            return resp!.Data;
        }

        public void Create(Booking b)
        {
            using var client = Connect();
            var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage
            {
                Command = "create",
                Booking = b
            });
            ReadResponse(stream); // אפשר לעבד תשובה/סטטוס אם רוצים
        }

        public void Update(Booking b)
        {
            using var client = Connect();
            var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage
            {
                Command = "update",
                Booking = b
            });
            ReadResponse(stream); // אפשר לעבד תשובה/סטטוס אם רוצים
        }

        public void Delete(int id)
        {
            using var client = Connect();
            var stream = client.GetStream();

            WriteRequest(stream, new RequestMessage
            {
                Command = "delete",
                Id = id
            });
            ReadResponse(stream); // אפשר לעבד תשובה/סטטוס אם רוצים
        }
    }
}
