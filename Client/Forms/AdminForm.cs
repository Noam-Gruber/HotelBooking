using Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class AdminForm : Form
    {
        private BookingApi api = new BookingApi();

        public AdminForm()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            var bookings = api.GetAll();
            dataGridView.DataSource = bookings;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            //dataGridView.DataSource = db.Bookings.ToList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView.Rows[e.RowIndex].DataBoundItem as Booking;
            if (row != null)
            {
                api.Update(row);
                MessageBox.Show("ההזמנה עודכנה בהצלחה!");
            }
        }
    }
}
