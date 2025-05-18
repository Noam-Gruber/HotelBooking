using System;
using System.Windows.Forms;

namespace Client.Forms
{
    /// <summary>
    /// Represents the login form for the application.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginForm"/> class.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        /// <summary>
        /// Handles the Login button click event. Authenticates the user and opens the appropriate form.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
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
                var bookingForm = new BookingForm();
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
