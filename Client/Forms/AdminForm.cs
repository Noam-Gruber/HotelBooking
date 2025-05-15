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
        /// API for booking operations.
        /// </summary>
        private BookingApi api = new BookingApi();

        /// <summary>
        /// Binding source for the DataGridView.
        /// </summary>
        private BindingSource bindingSource = new BindingSource();

        /// <summary>
        /// The current list of bookings loaded from the server.
        /// </summary>
        private List<Booking> currentBookings = new List<Booking>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminForm"/> class.
        /// </summary>
        public AdminForm()
        {
            InitializeComponent();

            // Event wiring (if not connected in Designer)
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnAdd.Click += btnAdd_Click;
            btnRefresh.Click += btnRefresh_Click;
            dataGridView.CellEndEdit += dataGridView_CellEndEdit;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            dataGridView.RowPrePaint += DataGridView_RowPrePaint;

            SetupDataGridView();
            LoadBookings();
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
                MessageBox.Show("אירעה שגיאה בטעינת ההזמנות:\n" + ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var addForm = new BookingForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // The booking is already sent to the server from BookingForm, no need for api.Create!
                LoadBookings();
                MessageBox.Show("ההזמנה נוספה בהצלחה!", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("ההזמנה עודכנה בהצלחה!", "עדכון", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("אירעה שגיאה בעת עדכון: " + ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    var confirm = MessageBox.Show("האם למחוק את ההזמנה?", "מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            api.Delete(row.Id);
                            LoadBookings();
                            MessageBox.Show("ההזמנה נמחקה בהצלחה.", "מחיקה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("אירעה שגיאה בעת מחיקה: " + ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
