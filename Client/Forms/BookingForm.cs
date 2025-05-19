using Common;
using System;
using System.IO;
using System.Text;
using System.Drawing;
using Common.Entities;
using Common.Messages;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    /// <summary>
    /// Form for creating a new booking or editing an existing booking.
    /// </summary>
    public partial class BookingForm : Form
    {
        /// <summary>
        /// Gets the newly created booking after a successful save.
        /// </summary>
        public Booking NewBooking { get; private set; }

        /// <summary>
        /// The server host address.
        /// </summary>
        private readonly string SERVER_HOST = Params.GetServerAddress();

        /// <summary>
        /// The server port number.
        /// </summary>
        private readonly int SERVER_PORT = Params.GetPort();

        /// <summary>
        /// The current index of the hotel image being displayed.
        /// </summary>
        private int currentImageIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingForm"/> class.
        /// </summary>
        public BookingForm()
        {
            InitializeComponent();
            cmbRoomType.SelectedIndex = 0;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(2);
        }


        /// <summary>
        /// Handles the Save button click event. Validates input, sends the booking to the server, and processes the response.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSave_Click(object sender, EventArgs e)
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

                // Send the booking request to the server (short connection for each operation)
                using var client = new TcpClient();
                client.Connect(SERVER_HOST, SERVER_PORT);
                using var stream = client.GetStream();

                var request = new RequestMessage
                {
                    Command = "create",
                    Booking = booking
                };

                var json = JsonConvert.SerializeObject(request);
                var payload = Encoding.UTF8.GetBytes(json);
                var length = BitConverter.GetBytes(payload.Length);
                stream.Write(length, 0, 4);
                stream.Write(payload, 0, payload.Length);

                // Receive the response
                var lenBuf = new byte[4];
                FillBuffer(stream, lenBuf);
                int respLen = BitConverter.ToInt32(lenBuf, 0);

                var respBuf = new byte[respLen];
                FillBuffer(stream, respBuf);

                var respJson = Encoding.UTF8.GetString(respBuf);
                var resp = JsonConvert.DeserializeObject<ResponseMessage<Booking>>(respJson);

                if (resp.Status == 201)
                {
                    NewBooking = resp.Data;
                    MessageBox.Show("Booking completed successfully! (ID=" + NewBooking.Id + ")", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An error occurred: " + resp.Data, "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Client error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reads the specified number of bytes from the network stream into the buffer.
        /// </summary>
        /// <param name="stream">The network stream to read from.</param>
        /// <param name="buffer">The buffer to fill with data.</param>
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
        /// Builds a <see cref="Booking"/> object from the current UI field values.
        /// </summary>
        /// <returns>A <see cref="Booking"/> object populated from the form fields.</returns>
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
        /// Populates the UI fields from the given <see cref="Booking"/> object.
        /// </summary>
        /// <param name="b">The booking to populate the UI from.</param>
        public void PopulateUiFromBooking(Booking b)
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
        /// Validates the email address when the email textbox loses focus.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
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
        /// Validates the check-in date when the value changes.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCheckIn.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Check-in date cannot be in the future.");
            }
        }

        /// <summary>
        /// Restricts the card number textbox to digits only.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The key press event arguments.</param>
        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // 8 is backspace
            {
                e.Handled = true;
            }
        }
    }
}