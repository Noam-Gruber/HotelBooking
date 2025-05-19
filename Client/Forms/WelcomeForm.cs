using System;
using System.IO;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Forms
{
    /// <summary>
    /// The welcome form displayed to users upon application start.
    /// Shows a slideshow, rotating quotes, and plays a welcome sound.
    /// </summary>
    public partial class WelcomeForm : Form
    {
        /// <summary>
        /// The current index of the hotel image being displayed.
        /// </summary>
        private int currentImage = 0;

        /// <summary>
        /// The current index of the quote being displayed.
        /// </summary>
        private int currentQuote = 0;

        /// <summary>
        /// Array of hotel image file paths for the slideshow.
        /// </summary>
        private string[] hotelImages = new string[]
        {
            Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Images\hotel1.png"),
            Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Images\hotel2.png"),
            Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName, @"Images\hotel3.png")
        };

        /// <summary>
        /// Array of customer quotes to display.
        /// </summary>
        private string[] quotes = new string[]
        {
            "\"Best stay ever!\" – John D.",
            "\"Lovely rooms, great service.\" – Anna T.",
            "\"Would book again!\" – Michael R.",
            "\"Perfect location and staff.\" – Sarah K."
        };

        /// <summary>
        /// Timer for rotating quotes.
        /// </summary>
        private Timer quoteTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeForm"/> class.
        /// </summary>
        public WelcomeForm()
        {
            InitializeComponent();
            pictureBoxSlideshow.Image = Image.FromFile(hotelImages[currentImage]);
            lblQuote.Text = quotes[0];
            StartSlideshow();
            StartQuoteRotation();
            PlayWelcomeSound();
        }

        /// <summary>
        /// Starts the hotel image slideshow in the form.
        /// </summary>
        private void StartSlideshow()
        {
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (sender, e) =>
            {
                if (currentImage >= hotelImages.Length)
                    currentImage = 0;
                pictureBoxSlideshow.Image = Image.FromFile(hotelImages[currentImage]);
                currentImage++;
            };
            timer.Start();
        }

        /// <summary>
        /// Starts the rotation of customer quotes in the form.
        /// </summary>
        private void StartQuoteRotation()
        {
            lblQuote.Text = quotes[0];
            quoteTimer = new Timer();
            quoteTimer.Interval = 3500;
            quoteTimer.Tick += (s, e) =>
            {
                currentQuote = (currentQuote + 1) % quotes.Length;
                lblQuote.Text = quotes[currentQuote];
            };
            quoteTimer.Start();
        }

        /// <summary>
        /// Plays the welcome sound when the form loads.
        /// </summary>
        private void PlayWelcomeSound()
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "welcome.wav");
                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to play the welcome sound: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the New Booking button click event. Opens the booking form.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnNewBooking_Click(object sender, EventArgs e)
        {
            var bookingForm = new BookingForm();
            bookingForm.ShowDialog();
            this.Hide();
        }

        /// <summary>
        /// Handles the Admin Login button click event. Opens the admin login form.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new AdminForm();
            loginForm.FormClosed += (s, ev) =>
            {
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    var adminForm = new AdminForm();
                    adminForm.Show();
                    this.Hide();
                }
            };
            loginForm.Show();
        }

        /// <summary>
        /// Handles the MouseEnter event for the New Booking button. Changes the button color.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnNewBooking_MouseEnter(object sender, EventArgs e)
        {
            btnNewBooking.BackColor = Color.CornflowerBlue;
        }

        /// <summary>
        /// Handles the MouseLeave event for the New Booking button. Restores the button color.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnNewBooking_MouseLeave(object sender, EventArgs e)
        {
            btnNewBooking.BackColor = Color.DeepSkyBlue;
        }
    }
}
