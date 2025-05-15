using System;
using Common.Entities;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class AdminForm : Form
    {
        private BookingApi api = new BookingApi();
        private BindingSource bindingSource = new BindingSource();

        public AdminForm()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadBookings();
        }

        private void SetupDataGridView()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;

            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "מספר הזמנה", DataPropertyName = "Id", ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "אורח", DataPropertyName = "GuestName" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "אימייל", DataPropertyName = "Email" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "צ'ק-אין", DataPropertyName = "CheckInDate" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "צ'ק-אאוט", DataPropertyName = "CheckOutDate" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "סוג חדר", DataPropertyName = "RoomType" });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "מס' אורחים", DataPropertyName = "NumberOfGuests" });
            // אל תאפשר עריכת כרטיס אשראי ב-Grid:
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "מס' כרטיס", DataPropertyName = "CardNumber", ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "חודש תפוגה", DataPropertyName = "ExpiryMonth", ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "שנה תפוגה", DataPropertyName = "ExpiryYear", ReadOnly = true });

            // עמודת מחיקה (כפתור):
            var deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.HeaderText = "מחיקה";
            deleteBtn.Name = "colDelete";
            deleteBtn.Text = "מחק";
            deleteBtn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(deleteBtn);

            dataGridView.CellContentClick += dataGridView_CellContentClick;
            dataGridView.CellEndEdit += dataGridView_CellEndEdit;
        }

        private void LoadBookings()
        {
            var bookings = api.GetAll();
            bindingSource.DataSource = bookings;
            dataGridView.DataSource = bindingSource;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // אם השורה נערכה, לעדכן ב-DB
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

        // לחיצה על מחיקה
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
    }
}
