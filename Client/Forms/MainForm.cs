using System;
using Common;
using Common.Entities;
using System.IO;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Client
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// The server host address.
        /// </summary>
        private readonly string SERVER_HOST = Params.GetServerAddress();

        /// <summary>
        /// The server port number.
        /// </summary>
        private readonly int SERVER_PORT = Params.GetPort();

        private int currentImageIndex = 0;
        private string[] hotelImages = new string[]
        {
           Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Images\hotel1.png"),
           Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Images\hotel2.png"),
           Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Images\hotel3.png")
        };

        // Timer שמריץ את האנימציה
        private void StartSlideshow()
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (sender, e) =>
            {
                if (currentImageIndex >= hotelImages.Length)
                    currentImageIndex = 0;
                pictureBox.Image = Image.FromFile(hotelImages[currentImageIndex]);
                currentImageIndex++;
            };
            timer.Start();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            StartSlideshow();
            cmbRoomType.SelectedIndex = 0;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(2);
        }

        /// <summary>
        /// Handles the Save button click event to create a new booking.
        /// </summary>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGuestName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            try
            {
                var booking = BuildBookingFromUi();

                var request = new RequestMessage
                {
                    Command = "Create",
                    Booking = booking
                };

                var response = await SendRequestAsync(request);
                if (response.Status == 201 || response.Status == 0)   // Created / OK
                {
                    var created = JsonConvert.DeserializeObject<Booking>(
                        response.Data.ToString() ?? "{}");
                    MessageBox.Show($"Booked! New ID = {created.Id}",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error: {response.Data}",
                                    "Server Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Client Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Get button click event to retrieve a booking by ID.
        /// </summary>
        private async void btnGet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Invalid ID", "Validation",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var request = new RequestMessage
                {
                    Command = "Get",
                    Id = id
                };

                var response = await SendRequestAsync(request);
                if (response.Status == 0)                   // OK
                {
                    var booking = JsonConvert.DeserializeObject<Booking>(
                        response.Data.ToString() ?? "{}");
                    PopulateUiFromBooking(booking);
                }
                else
                {
                    MessageBox.Show($"Not Found (Status={response.Status})",
                                    "Info",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Client Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sends a request to the server and retrieves the response.
        /// </summary>
        /// <param name="req">The request message to send.</param>
        /// <returns>The response message from the server.</returns>
        private async Task<ResponseMessage> SendRequestAsync(RequestMessage req)
        {
            using var client = new TcpClient();
            await client.ConnectAsync(SERVER_HOST, SERVER_PORT);
            using var stream = client.GetStream();

            // ---- write (length-prefixed JSON) ----
            var json = JsonConvert.SerializeObject(req);
            var payload = Encoding.UTF8.GetBytes(json);
            var length = BitConverter.GetBytes(payload.Length); // 4 bytes
            await stream.WriteAsync(length, 0, 4);
            await stream.WriteAsync(payload, 0, payload.Length);

            // ---- read response ----
            var lenBuf = new byte[4];
            await FillBufferAsync(stream, lenBuf);
            int respLen = BitConverter.ToInt32(lenBuf, 0);

            var respBuf = new byte[respLen];
            await FillBufferAsync(stream, respBuf);

            var respJson = Encoding.UTF8.GetString(respBuf);
            return JsonConvert.DeserializeObject<ResponseMessage>(respJson);
        }

        /// <summary>
        /// Fills the buffer with data from the network stream.
        /// </summary>
        /// <param name="s">The network stream.</param>
        /// <param name="buffer">The buffer to fill.</param>
        private static async Task FillBufferAsync(NetworkStream s, byte[] buffer)
        {
            int read = 0;
            while (read < buffer.Length)
            {
                int n = await s.ReadAsync(buffer, read, buffer.Length - read);
                if (n == 0) throw new Exception("Connection closed");
                read += n;
            }
        }

        /// <summary>
        /// Builds a <see cref="Booking"/> object from the UI input fields.
        /// </summary>
        /// <returns>A <see cref="Booking"/> object populated with UI data.</returns>
        private Booking BuildBookingFromUi() =>
            new Booking
            {
                GuestName = txtGuestName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                CheckInDate = dtpCheckIn.Value.Date,
                CheckOutDate = dtpCheckOut.Value.Date,
                RoomType = cmbRoomType.SelectedItem?.ToString() ?? "Single",
                NumberOfGuests = (int)numGuests.Value,
                CardNumber = txtCardNumber.Text.Trim(),
                ExpiryMonth = int.TryParse(txtExpiryMonth.Text, out var m) ? m : 0,
                ExpiryYear = int.TryParse(txtExpiryYear.Text, out var y) ? y : 0
            };

        /// <summary>
        /// Populates the UI fields with data from a <see cref="Booking"/> object.
        /// </summary>
        /// <param name="b">The <see cref="Booking"/> object to populate the UI with.</param>
        private void PopulateUiFromBooking(Booking b)
        {
            txtGuestName.Text = b.GuestName;
            txtEmail.Text = b.Email;
            dtpCheckIn.Value = b.CheckInDate;
            dtpCheckOut.Value = b.CheckOutDate;
            cmbRoomType.SelectedItem = b.RoomType;
            numGuests.Value = b.NumberOfGuests;
            txtCardNumber.Text = b.CardNumber;
            txtExpiryMonth.Text = b.ExpiryMonth.ToString();
            txtExpiryYear.Text = b.ExpiryYear.ToString();
        }

        /// <summary>
        /// Validates the email address when the email text box loses focus.
        /// </summary>
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show("Please enter a valid email address.");
            }
        }

        /// <summary>
        /// Validates the check-in date when the date picker value changes.
        /// </summary>
        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCheckIn.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Check-in date cannot be in the future.");
            }
        }

        /// <summary>
        /// Ensures only numeric input is allowed in the card number text box.
        /// </summary>
        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 is backspace
            {
                e.Handled = true;
            }
        }
    }

    /// <summary>
    /// Represents a request message sent to the server.
    /// </summary>
    public class RequestMessage
    {
        /// <summary>
        /// The command to execute (e.g., "GetAll", "Get", "Create").
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The ID of the booking (used for "Get" command).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The booking data (used for "Create" command).
        /// </summary>
        public Booking Booking { get; set; }
    }

    /// <summary>
    /// Represents a response message received from the server.
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// The status code of the response (e.g., 0=OK, 1=NotFound, 2=Error, 201=Created).
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// The data associated with the response (e.g., Booking, List&lt;Booking&gt;, or string).
        /// </summary>
        public object Data { get; set; }
    }
}
