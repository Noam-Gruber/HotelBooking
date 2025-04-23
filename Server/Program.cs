using System;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Sockets;
using Server.Data;

namespace Server
{
    class Program
    {
        private const int PORT = 9000;

        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Loopback, PORT);
            listener.Start();
            Console.WriteLine($"TCP server listening on port {PORT}...");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(_ => HandleClient(client));
            }
        }

        static void HandleClient(TcpClient client)
        {
            try
            {
                using (client)
                using (var stream = client.GetStream())
                {
                    // קריאה של ההודעה מהקליינט
                    var req = ReadRequest(stream);
                    object result = null;
                    int statusCode = 0; // 0 = OK, 1 = NotFound, 2 = Error

                    try
                    {
                        using (var db = new ApplicationDbContext())
                        {
                            switch (req.Command?.ToLowerInvariant())
                            {
                                case "getall":
                                    result = db.Bookings.ToList();
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
                                    statusCode = 201; // created
                                    break;

                                default:
                                    statusCode = 2;
                                    result = $"Unknown command '{req.Command}'";
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        statusCode = 2;
                        result = ex.Message;
                    }

                    // שליחת תגובה
                    WriteResponse(stream, new ResponseMessage
                    {
                        Status = statusCode,
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Client disconnected.");
            }
        }

        // ---------- Business ----------
        private static (int status, object data) ProcessCommand(RequestMessage req)
        {
            try
            {
                using var db = new ApplicationDbContext();

                switch (req.Command?.ToLowerInvariant())
                {
                    case "getall":
                        return (0, db.Bookings.ToList());

                    case "get":
                        var item = db.Bookings.Find(req.Id);
                        return item == null ? (1, "Not found") : (0, item);

                    case "create":
                        db.Bookings.Add(req.Booking);
                        db.SaveChanges();
                        return (201, req.Booking);

                    default:
                        return (2, $"Unknown command '{req.Command}'");
                }
            }
            catch (Exception ex)
            {
                return (2, ex.Message);
            }
        }

        // ---------- Framing helpers ----------
        private static RequestMessage ReadRequest(NetworkStream s)
        {
            var lenBuf = new byte[4];
            s.Read(lenBuf, 0, 4);
            int len = BitConverter.ToInt32(lenBuf, 0);

            var buf = new byte[len];
            int read = 0;
            while (read < len) read += s.Read(buf, read, len - read);

            var json = Encoding.UTF8.GetString(buf);
            return JsonConvert.DeserializeObject<RequestMessage>(json);
        }

        private static void WriteResponse(NetworkStream s, ResponseMessage resp)
        {
            var json = JsonConvert.SerializeObject(resp);
            var buf = Encoding.UTF8.GetBytes(json);
            var len = BitConverter.GetBytes(buf.Length);
            s.Write(len, 0, 4);
            s.Write(buf, 0, buf.Length);
        }

    }
}
