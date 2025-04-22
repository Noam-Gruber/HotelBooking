using System;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var booking = BuildBookingFromUi();

                var request = new RequestMessage
                {
                    Command = "Create",
                    Booking = booking
                };

                var response = await SendRequestAsync(request);
                if (response.Status == 201 || response.Status == 0)
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
                if (response.Status == 0)
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

    }
}
