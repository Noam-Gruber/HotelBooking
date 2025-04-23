using System;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "admin" && password == "admin123")
            {
                MessageBox.Show("Welcome, Admin!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var adminForm = new AdminForm();
                adminForm.Show();
                this.Hide();
            }
            else if (username.Length > 0 && password.Length > 0)
            {
                MessageBox.Show("Welcome, Customer!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var bookingForm = new MainForm();
                bookingForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please enter a valid username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
