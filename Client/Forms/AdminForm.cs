using System;
using System.Linq;
using Common.Entities;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Client.Forms
{
    /// <summary>
    /// Represents the admin form for managing bookings and handling chat with customers.
    /// </summary>
    public partial class AdminForm : Form
    {
        /// <summary>
        /// The API service used for booking and chat operations.
        /// </summary>
        private readonly Services api;

        /// <summary>
        /// The binding source for the bookings data grid.
        /// </summary>
        private BindingSource bindingSource = new BindingSource();

        /// <summary>
        /// The current list of bookings loaded from the server.
        /// </summary>
        private List<Booking> currentBookings = new List<Booking>();

        /// <summary>
        /// Timer for periodically refreshing the admin chat.
        /// </summary>
        private Timer adminChatRefreshTimer;

        /// <summary>
        /// All chat messages loaded for the admin.
        /// </summary>
        private List<ChatMessage> allChatMessages = new List<ChatMessage>();

        /// <summary>
        /// The currently selected session ID for chat.
        /// </summary>
        private string selectedSessionId = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminForm"/> class.
        /// </summary>
        /// <param name="api">The API service for booking and chat operations.</param>
        public AdminForm(Services api)
        {
            InitializeComponent();
            this.api = api;
            SetupDataGridView();
            LoadBookings();
            StartAdminChatRefresh();

            // Listen for client selection changes
            comboBoxClients.SelectedIndexChanged += comboBoxClients_SelectedIndexChanged;
        }

        /// <summary>
        /// Configures the columns and settings for the bookings data grid view.
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
        /// Applies the search filter to the bookings list and updates the data grid.
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
        private void btnRefresh_Click(object sender, EventArgs e) => LoadBookings();

        /// <summary>
        /// Handles the TextChanged event for the search textbox to apply the filter.
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();

        /// <summary>
        /// Handles the Add button click event to open the booking form and add a new booking.
        /// </summary>
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
        /// Handles the CellEndEdit event for the data grid to update a booking after editing.
        /// </summary>
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
        /// Handles the CellContentClick event for the data grid to delete a booking when the delete button is clicked.
        /// </summary>
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
        /// Handles the RowPrePaint event to highlight future bookings in the data grid.
        /// </summary>
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

        // ---- Chat Section ----

        /// <summary>
        /// Starts the timer to periodically refresh the admin chat.
        /// </summary>
        private void StartAdminChatRefresh()
        {
            adminChatRefreshTimer = new Timer();
            adminChatRefreshTimer.Interval = 2000;
            adminChatRefreshTimer.Tick += (s, e) => RefreshAdminChat();
            adminChatRefreshTimer.Start();
            RefreshAdminChat();
        }

        /// <summary>
        /// Refreshes the admin chat messages and updates the UI.
        /// </summary>
        private void RefreshAdminChat()
        {
            try
            {
                var messages = api.GetAllChatMessages();
                allChatMessages = messages;
                UpdateClientsComboBox();
                UpdateAdminChatDisplay();
            }
            catch (Exception)
            {
                // Do not show error every 2 seconds
            }
        }

        /// <summary>
        /// Updates the clients combo box with all active session IDs.
        /// </summary>
        private void UpdateClientsComboBox()
        {
            var sessionIds = allChatMessages
                .Select(m => m.SessionId)
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .OrderBy(id => id)
                .ToList();

            string previousSelected = comboBoxClients.SelectedItem?.ToString();

            comboBoxClients.Items.Clear();
            foreach (var id in sessionIds)
                comboBoxClients.Items.Add(id);

            // Try to restore previous selection
            if (!string.IsNullOrEmpty(previousSelected) && sessionIds.Contains(previousSelected))
                comboBoxClients.SelectedItem = previousSelected;
            // If no selection, select the first (if exists)
            else if (comboBoxClients.Items.Count > 0 && comboBoxClients.SelectedIndex == -1)
                comboBoxClients.SelectedIndex = 0;
        }

        /// <summary>
        /// Updates the chat display for the selected client session.
        /// </summary>
        private void UpdateAdminChatDisplay()
        {
            richTextBox1.Clear();
            selectedSessionId = comboBoxClients.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedSessionId))
            {
                richTextBox1.AppendText("Select a client from the combo box to view the conversation...");
                return;
            }

            var messages = allChatMessages
                .Where(m => m.SessionId == selectedSessionId)
                .OrderBy(m => m.Timestamp)
                .ToList();

            foreach (var msg in messages)
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;

                // Different color for each side
                if (msg.IsFromAdmin)
                    richTextBox1.SelectionBackColor = System.Drawing.Color.LightBlue;
                else
                    richTextBox1.SelectionBackColor = System.Drawing.Color.LightGray;

                // Name + message
                string prefix = msg.IsFromAdmin ? "🛎️ Admin" : "👤 Guest";
                richTextBox1.SelectionFont = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

                richTextBox1.AppendText($"{prefix}: ");

                // Regular text (not name)
                richTextBox1.SelectionFont = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
                richTextBox1.AppendText($"{msg.Message} ");
                richTextBox1.SelectionFont = new System.Drawing.Font("Segoe UI", 8, System.Drawing.FontStyle.Italic);
                richTextBox1.AppendText($"({msg.Timestamp:HH:mm})\n");

                // Reset color
                richTextBox1.SelectionBackColor = richTextBox1.BackColor;
            }
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        /// <summary>
        /// Sends an admin message to the selected client session.
        /// </summary>
        private void SendAdminMessage()
        {
            string messageText = textBox_message.Text.Trim();
            if (string.IsNullOrEmpty(messageText))
                return;

            string targetSessionId = comboBoxClients.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(targetSessionId))
            {
                MessageBox.Show("Please select a client to send the message to!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var message = new ChatMessage
                {
                    SenderName = "Admin",
                    Message = messageText,
                    IsFromAdmin = true,
                    SessionId = targetSessionId // Send to the specific session
                };

                api.SendChatMessage(message);
                textBox_message.Clear();
                RefreshAdminChat(); // Immediate refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send admin message: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Send button click event to send an admin chat message.
        /// </summary>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendAdminMessage();
        }

        /// <summary>
        /// Handles the KeyPress event for the message textbox to send a message on Enter key.
        /// </summary>
        private void textBox_message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendAdminMessage();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event for the clients combo box to update the chat display.
        /// </summary>
        private void comboBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAdminChatDisplay();
        }

        /// <summary>
        /// Handles the Delete Old Chats button click event to delete chats older than 5 minutes.
        /// </summary>
        private void btnDeleteOldChats_Click(object sender, EventArgs e)
        {
            try
            {
                api.DeleteOldChats(5); // Delete chats not active for over 5 minutes
                RefreshAdminChat();
                MessageBox.Show("Old chats deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting chats: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
