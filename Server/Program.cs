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
        const int Port = 9000;

        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Loopback, Port);
            listener.Start();
            Console.WriteLine($"TCP server listening on port {Port}...");

            while (true)
            {
                var client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
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

        /// <summary>
        /// קורא הודעה עם framing: 4 בייטים ראשונים = אורך payload,
        /// לאחר מכן JSON payload.
        /// </summary>
        static RequestMessage ReadRequest(NetworkStream stream)
        {
            var lenBuf = new byte[4];
            stream.Read(lenBuf, 0, 4);
            int length = BitConverter.ToInt32(lenBuf, 0);

            var buf = new byte[length];
            int read = 0;
            while (read < length)
                read += stream.Read(buf, read, length - read);

            var json = Encoding.UTF8.GetString(buf);
            return JsonConvert.DeserializeObject<RequestMessage>(json);
        }

        /// <summary>
        /// שולח תגובה במבנה דומה: 4 בייטים של אורך + JSON.
        /// </summary>
        static void WriteResponse(NetworkStream stream, ResponseMessage resp)
        {
            var json = JsonConvert.SerializeObject(resp);
            var buf = Encoding.UTF8.GetBytes(json);
            var len = BitConverter.GetBytes(buf.Length);
            stream.Write(len, 0, 4);
            stream.Write(buf, 0, buf.Length);
        }
    }
}
