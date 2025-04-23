using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Newtonsoft.Json;

namespace Client
{
    public partial class MainForm : Form
    {
        private readonly string SERVER_HOST = Params.GetServerAddress();
        private readonly int SERVER_PORT = Params.GetPort();

        public MainForm()
        {
            InitializeComponent();
            cmbRoomType.SelectedIndex = 0;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(2);
        }

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

        // ========== UI ⇆ Model ==========
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

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show("Please enter a valid email address.");
            }
        }

        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCheckIn.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Check-in date cannot be in the future.");
            }
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 is backspace
            {
                e.Handled = true;
            }
        }
    }

    // ---------- DTOs (העבירו אחר-כך ל-Common) ----------
    public class RequestMessage
    {
        public string Command { get; set; }   // "GetAll", "Get", "Create"
        public int Id { get; set; }   // עבור "Get"
        public Booking Booking { get; set; }   // עבור "Create"
    }

    public class ResponseMessage
    {
        public int Status { get; set; }     // 0=OK,1=NotFound,2=Error,201=Created
        public object Data { get; set; }     // Booking / List<Booking> / string
    }
}
