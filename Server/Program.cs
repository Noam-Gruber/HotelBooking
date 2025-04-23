using System;
using System.Net;
using System.Linq;
using System.Text;
using Server.Data;
using Newtonsoft.Json;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Entry point for the server application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The port number the server listens on.
        /// </summary>
        private static readonly int port = Common.Params.GetPort();

        /// <summary>
        /// Main method to start the TCP server.
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
        /// Handles communication with a connected client.
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
        /// Processes a command from the client and interacts with the database.
        /// </summary>
        /// <param name="req">The request message containing the command and data.</param>
        /// <returns>A tuple containing the status code and the result data.</returns>
        private static (int status, object data) ProcessCommand(RequestMessage req)
        {
            int statusCode = 0;

            try
            {
                using var db = new ApplicationDbContext();

                switch (req.Command?.ToLowerInvariant())
                {
                    case "getall":
                        return (statusCode, db.Bookings.ToList());

                    case "get":
                        var item = db.Bookings.Find(req.Id);
                        return item == null ? (1, "Not found") : (statusCode, item);

                    case "create":
                        db.Bookings.Add(req.Booking);
                        db.SaveChanges();
                        statusCode = 201; // Created
                        return (statusCode, req.Booking);

                    case "update":
                        var updateItem = db.Bookings.Find(req.Id);
                        if (updateItem != null)
                        {
                            updateItem.GuestName = req.Booking.GuestName;
                            db.SaveChanges();
                            statusCode = 200; // OK
                            return (statusCode, updateItem);
                        }
                        else
                        {
                            statusCode = 1; // Not Found
                            return (statusCode, "Booking not found for update");
                        }

                    case "delete":
                        var deleteItem = db.Bookings.Find(req.Id);
                        if (deleteItem != null)
                        {
                            db.Bookings.Remove(deleteItem);
                            db.SaveChanges();
                            statusCode = 204; // No Content
                            return (statusCode, "Booking deleted");
                        }
                        else
                        {
                            statusCode = 1; // Not Found
                            return (statusCode, "Booking not found for deletion");
                        }

                    default:
                        return (2, $"Unknown command '{req.Command}'");
                }
            }
            catch (Exception ex)
            {
                return (2, ex.Message);
            }
        }

        /// <summary>
        /// Reads a request message from the network stream.
        /// </summary>
        /// <param name="s">The network stream to read from.</param>
        /// <returns>The deserialized request message.</returns>
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
        /// Writes a response message to the network stream.
        /// </summary>
        /// <param name="s">The network stream to write to.</param>
        /// <param name="resp">The response message to send.</param>
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
