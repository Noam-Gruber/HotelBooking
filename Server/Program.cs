using System;
using System.Net;
using Server.Data;
using System.Text;
using System.Linq;
using Common.Entities;
using Common.Messages;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Entry point for the server application. Handles incoming TCP client connections and processes booking requests.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The TCP port on which the server listens for incoming connections.
        /// </summary>
        private static readonly int port = Common.Params.GetPort();

        /// <summary>
        /// Main entry point. Starts the TCP listener and handles incoming client connections.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
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

        /// <summary>
        /// Handles a single client connection, processes the request, and sends a response.
        /// </summary>
        /// <param name="client">The connected TCP client.</param>
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

        /// <summary>
        /// Reads a request message from the network stream.
        /// </summary>
        /// <param name="s">The network stream to read from.</param>
        /// <returns>The deserialized <see cref="RequestMessage"/> object.</returns>
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

        /// <summary>
        /// Serializes and writes a response message to the network stream.
        /// </summary>
        /// <typeparam name="T">The type of the data in the response.</typeparam>
        /// <param name="s">The network stream to write to.</param>
        /// <param name="resp">The response message to send.</param>
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
