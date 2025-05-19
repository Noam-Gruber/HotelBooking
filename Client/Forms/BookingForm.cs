using System;
using Common.Entities;
using System.Windows.Forms;

namespace Client
{
    /// <summary>
    /// Represents the form for creating a new booking or editing an existing booking.
    /// </summary>
    public partial class BookingForm : Form
    {
        /// <summary>
        /// The API service used for booking operations.
        /// </summary>
        private readonly Services api;

        /// <summary>
        /// Gets the newly created booking after a successful save.
        /// </summary>
        public Booking NewBooking { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingForm"/> class.
        /// </summary>
        /// <param name="api">The API service for booking operations.</param>
        public BookingForm(Services api)
        {
            InitializeComponent();
            this.api = api;
            cmbRoomType.SelectedIndex = 0;
            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(2);
            UpdateSummary();
        }

        /// <summary>
        /// Handles the Save button click event. Validates input and creates a new booking.
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
                api.CreateBooking(booking);
                MessageBox.Show("Booking completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.NewBooking = booking;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Client error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Builds a <see cref="Booking"/> object from the current UI input values.
        /// </summary>
        /// <returns>A <see cref="Booking"/> object populated with form data.</returns>
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
        /// Updates the booking summary label with the current form values.
        /// </summary>
        private void UpdateSummary()
        {
            lblSummary.Text = $"Booking: {numGuests.Value} guests, {cmbRoomType.SelectedItem}, " +
                              $"{dtpCheckIn.Value:dd/MM/yyyy} - {dtpCheckOut.Value:dd/MM/yyyy}";
        }

        /// <summary>
        /// Handles the Leave event for the email textbox. Validates the email format.
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
        /// Handles the KeyPress event for the card number textbox. Allows only digits and backspace.
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

        /// <summary>
        /// Handles events that require updating the booking summary label.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void UpdateSummary(object sender, EventArgs e)
        {
            UpdateSummary();
        }
    }
}
