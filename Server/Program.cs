using Common.Entities;
using Common.Messages;
using Newtonsoft.Json;
using Server.Data;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        private static readonly int port = Common.Params.GetPort();

        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            Console.WriteLine($"TCP server listening on port {port}...");

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
                    var req = ReadRequest(stream);

                    try
                    {
                        using (var db = new ApplicationDbContext())
                        {
                            switch (req.Command?.ToLowerInvariant())
                            {
                                case "getall":
                                    var list = db.Bookings.OrderBy(b => b.CheckInDate).ToList();
                                    WriteResponse(stream, new ResponseMessage<System.Collections.Generic.List<Booking>>
                                    {
                                        Status = 0,
                                        Data = list
                                    });
                                    break;

                                case "get":
                                    var item = db.Bookings.Find(req.Id);
                                    if (item == null)
                                    {
                                        WriteResponse(stream, new ResponseMessage<string>
                                        {
                                            Status = 1,
                                            Data = "Booking not found"
                                        });
                                    }
                                    else
                                    {
                                        WriteResponse(stream, new ResponseMessage<Booking>
                                        {
                                            Status = 0,
                                            Data = item
                                        });
                                    }
                                    break;

                                case "create":
                                    try
                                    {
                                        db.Bookings.Add(req.Booking);
                                        db.SaveChanges();
                                        WriteResponse(stream, new ResponseMessage<Booking>
                                        {
                                            Status = 201,
                                            Data = req.Booking
                                        });
                                    }
                                    catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                                    {
                                        var baseErr = ex.GetBaseException();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"[EF] {baseErr.Message}");
                                        Console.ResetColor();
                                        WriteResponse(stream, new ResponseMessage<string>
                                        {
                                            Status = 2,
                                            Data = baseErr.Message
                                        });
                                    }
                                    break;

                                case "update":
                                    var updateItem = db.Bookings.Find(req.Booking.Id);
                                    if (updateItem != null)
                                    {
                                        updateItem.GuestName = req.Booking.GuestName;
                                        // ... תעדכן פה עוד שדות לפי הצורך
                                        db.SaveChanges();
                                        WriteResponse(stream, new ResponseMessage<Booking>
                                        {
                                            Status = 200,
                                            Data = updateItem
                                        });
                                    }
                                    else
                                    {
                                        WriteResponse(stream, new ResponseMessage<string>
                                        {
                                            Status = 1,
                                            Data = "Booking not found for update"
                                        });
                                    }
                                    break;

                                case "delete":
                                    var deleteItem = db.Bookings.Find(req.Id);
                                    if (deleteItem != null)
                                    {
                                        db.Bookings.Remove(deleteItem);
                                        db.SaveChanges();
                                        WriteResponse(stream, new ResponseMessage<string>
                                        {
                                            Status = 204,
                                            Data = "Booking deleted"
                                        });
                                    }
                                    else
                                    {
                                        WriteResponse(stream, new ResponseMessage<string>
                                        {
                                            Status = 1,
                                            Data = "Booking not found for deletion"
                                        });
                                    }
                                    break;

                                default:
                                    WriteResponse(stream, new ResponseMessage<string>
                                    {
                                        Status = 2,
                                        Data = $"Unknown command '{req.Command}'"
                                    });
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteResponse(stream, new ResponseMessage<string>
                        {
                            Status = 2,
                            Data = ex.Message
                        });
                    }
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

        private static void WriteResponse<T>(NetworkStream s, ResponseMessage<T> resp)
        {
            var json = JsonConvert.SerializeObject(resp);
            var buf = Encoding.UTF8.GetBytes(json);
            var len = BitConverter.GetBytes(buf.Length);
            s.Write(len, 0, 4);
            s.Write(buf, 0, buf.Length);
        }
    }
}
