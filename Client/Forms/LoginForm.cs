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
        /// The shared Services instance for API operations.
        /// </summary>
        private readonly Services api;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginForm"/> class.
        /// </summary>
        /// <param name="api">The shared <see cref="Services"/> instance for API operations.</param>
        public LoginForm(Services api)
        {
            InitializeComponent();
            this.api = api;
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
                using (var adminForm = new AdminForm(api))
                {
                    this.Hide();
                    adminForm.ShowDialog();
                    this.Show();
                }
            }
            else if (username.Length > 0 && password.Length > 0)
            {
                MessageBox.Show("Welcome, Customer!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (var bookingForm = new BookingForm(api))
                {
                    this.Hide();
                    bookingForm.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
