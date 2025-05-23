using Common.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
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
        /// API service for booking operations.
        /// </summary>
        private Services api = new Services();

        /// <summary>
        /// The current index of the hotel image being displayed.
        /// </summary>
        private int currentImage = 0;

        /// <summary>
        /// The current index of the quote being displayed.
        /// </summary>
        private int currentQuote = 0;

        private string currentSessionId;
        private Timer chatRefreshTimer;
        private List<ChatMessage> currentMessages = new List<ChatMessage>();
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
            currentSessionId = Guid.NewGuid().ToString(); // יצירת סשן ייחודי
            StartChatRefresh();
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
            using (var bookingForm = new BookingForm(api))
            {
                bookingForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Handles the Admin Login button click event. Opens the admin login form.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            using (var loginForm = new LoginForm(api))
            {
                // Show the login form as a dialog. If login is successful (DialogResult.OK), open the admin form.
                if (loginForm.ShowDialog(this) == DialogResult.OK)
                {
                    using (var adminForm = new AdminForm(api))
                    {
                        adminForm.ShowDialog(this);
                    }
                }
            }
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

        /// <summary>
        /// מתחיל לרענן את הצ'אט כל כמה שניות
        /// </summary>
        private void StartChatRefresh()
        {
            chatRefreshTimer = new Timer();
            chatRefreshTimer.Interval = 3000; // כל 3 שניות
            chatRefreshTimer.Tick += (s, e) => RefreshChat();
            chatRefreshTimer.Start();
        }

        /// <summary>
        /// מרענן את הודעות הצ'אט
        /// </summary>
        private void RefreshChat()
        {
            try
            {
                var messages = api.GetChatMessages(currentSessionId);
                if (messages.Count != currentMessages.Count)
                {
                    currentMessages = messages;
                    UpdateChatDisplay();
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה, לא מציגים הודעה כי זה רק רענון
            }
        }

        /// <summary>
        /// מעדכן את התצוגה של הצ'אט
        /// </summary>
        private void UpdateChatDisplay()
        {
            richTextBox1.Clear();
            foreach (var msg in currentMessages)
            {
                string prefix = msg.IsFromAdmin ? "[Admin]" : "[You]";
                richTextBox1.AppendText($"{prefix} {msg.SenderName}: {msg.Message}\n");
            }

            // גלילה למטה
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        /// <summary>
        /// שליחת הודעה בצ'אט
        /// </summary>
        private void SendChatMessage()
        {
            string messageText = textBox_message.Text.Trim();
            if (string.IsNullOrEmpty(messageText))
                return;

            try
            {
                var message = new ChatMessage
                {
                    SenderName = "Guest",
                    Message = messageText,
                    IsFromAdmin = false,
                    SessionId = currentSessionId
                };

                api.SendChatMessage(message);
                textBox_message.Clear();
                RefreshChat(); // רענון מיידי
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send message: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendChatMessage();
        }

        private void textBox_message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendChatMessage();
                e.Handled = true;
            }
        }
    }
}
