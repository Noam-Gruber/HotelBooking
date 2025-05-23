using System;
using System.Linq;
using Common.Entities;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Client.Forms
{
    /// <summary>
    /// Represents the administration form for managing bookings.
    /// </summary>
    public partial class AdminForm : Form
    {
        /// <summary>
        /// The API service used for booking operations.
        /// </summary>
        private readonly Services api;

        /// <summary>
        /// The binding source for the DataGridView.
        /// </summary>
        private BindingSource bindingSource = new BindingSource();

        /// <summary>
        /// The current list of bookings loaded from the server.
        /// </summary>
        private List<Booking> currentBookings = new List<Booking>();
        private Timer adminChatRefreshTimer;
        private List<ChatMessage> allChatMessages = new List<ChatMessage>();
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminForm"/> class.
        /// </summary>
        /// <param name="api">The API service for booking operations.</param>
        public AdminForm(Services api)
        {
            InitializeComponent();
            this.api = api;
            SetupDataGridView();
            LoadBookings();
            StartAdminChatRefresh();
        }

        /// <summary>
        /// Configures the columns and properties of the DataGridView.
        /// </summary>
        private void SetupDataGridView()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;

            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Id", DataPropertyName = "Id", ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Guest Name", DataPropertyName = "GuestName" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Check In Date", DataPropertyName = "CheckInDate" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Check Out Date", DataPropertyName = "CheckOutDate" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "RoomType", DataPropertyName = "RoomType" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Number Of Guests", DataPropertyName = "NumberOfGuests" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Card Number", DataPropertyName = "CardNumber", ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Expiry Month", DataPropertyName = "ExpiryMonth", ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Expiry Year", DataPropertyName = "ExpiryYear", ReadOnly = true });

            var deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.Name = "colDelete";
            deleteBtn.Text = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(deleteBtn);

            dataGridView.CellContentClick += dataGridView_CellContentClick;
            dataGridView.CellEndEdit += dataGridView_CellEndEdit;
            dataGridView.RowPrePaint += DataGridView_RowPrePaint;
        }

        /// <summary>
        /// Loads all bookings from the server and applies the current filter.
        /// </summary>
        private void LoadBookings()
        {
            try
            {
                currentBookings = api.GetAll();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading bookings:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Applies the search filter to the bookings and updates the DataGridView.
        /// </summary>
        private void ApplyFilter()
        {
            string search = txtSearch.Text.Trim().ToLower();
            var filtered = currentBookings.Where(b =>
                b.GuestName.ToLower().Contains(search) ||
                b.Email.ToLower().Contains(search) ||
                b.Id.ToString().Contains(search)
            ).ToList();
            bindingSource.DataSource = filtered;
            dataGridView.DataSource = bindingSource;
        }

        /// <summary>
        /// Handles the Refresh button click event to reload bookings.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRefresh_Click(object sender, EventArgs e) => LoadBookings();

        /// <summary>
        /// Handles the TextChanged event of the search textbox to filter bookings.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();

        /// <summary>
        /// Handles the Add button click event to add a new booking.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var bookingForm = new BookingForm(api))
            {
                if (bookingForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBookings();
                    MessageBox.Show("Booking added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event to update a booking after editing.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView.Rows[e.RowIndex].DataBoundItem as Booking;
            if (row != null)
            {
                try
                {
                    api.Update(row);
                    MessageBox.Show("Booking updated successfully!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event to delete a booking when the delete button is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["colDelete"].Index && e.RowIndex >= 0)
            {
                var row = dataGridView.Rows[e.RowIndex].DataBoundItem as Booking;
                if (row != null)
                {
                    var confirm = MessageBox.Show("Are you sure you want to delete this booking?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            api.Delete(row.Id);
                            LoadBookings();
                            MessageBox.Show("Booking deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred while deleting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Highlights future bookings in green in the DataGridView.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = dataGridView.Rows[e.RowIndex].DataBoundItem as Booking;
            if (row != null && row.CheckInDate > DateTime.Now)
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// מתחיל לרענן את הצ'אט של האדמין
        /// </summary>
        private void StartAdminChatRefresh()
        {
            adminChatRefreshTimer = new Timer();
            adminChatRefreshTimer.Interval = 2000; // כל 2 שניות
            adminChatRefreshTimer.Tick += (s, e) => RefreshAdminChat();
            adminChatRefreshTimer.Start();
            RefreshAdminChat(); // רענון ראשוני
        }

        /// <summary>
        /// מרענן את הודעות הצ'אט של האדמין
        /// </summary>
        private void RefreshAdminChat()
        {
            try
            {
                var messages = api.GetAllChatMessages();
                if (messages.Count != allChatMessages.Count)
                {
                    allChatMessages = messages;
                    UpdateAdminChatDisplay();
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה, לא מציגים הודעה
            }
        }

        /// <summary>
        /// מעדכן את התצוגה של הצ'אט של האדמין
        /// </summary>
        private void UpdateAdminChatDisplay()
        {
            richTextBox1.Clear();

            // קיבוץ הודעות לפי סשן
            var grouped = allChatMessages.GroupBy(m => m.SessionId ?? "unknown").ToList();

            foreach (var group in grouped)
            {
                richTextBox1.AppendText($"=== Session: {group.Key} ===\n");
                foreach (var msg in group.OrderBy(m => m.Timestamp))
                {
                    string prefix = msg.IsFromAdmin ? "[Admin]" : "[Guest]";
                    richTextBox1.AppendText($"{prefix} {msg.SenderName}: {msg.Message} ({msg.Timestamp:HH:mm})\n");
                }
                richTextBox1.AppendText("\n");
            }

            // גלילה למטה
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        /// <summary>
        /// שליחת הודעה כאדמין
        /// </summary>
        private void SendAdminMessage()
        {
            string messageText = textBox_message.Text.Trim();
            if (string.IsNullOrEmpty(messageText))
                return;

            try
            {
                // שליחה לכל הסשנים הפעילים או לסשן מסוים
                var message = new ChatMessage
                {
                    SenderName = "Admin",
                    Message = messageText,
                    IsFromAdmin = true,
                    SessionId = null // הודעה גלובלית
                };

                api.SendChatMessage(message);
                textBox_message.Clear();
                RefreshAdminChat(); // רענון מיידי
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send admin message: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendAdminMessage();
        }

        private void textBox_message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendAdminMessage();
                e.Handled = true;
            }
        }
    }
}
