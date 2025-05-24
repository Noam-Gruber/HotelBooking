using System;
using System.IO;
using System.Media;
using System.Drawing;
using Common.Entities;
using System.Windows.Forms;
using System.Collections.Generic;

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

        /// <summary>
        /// The unique session ID for the current chat session.
        /// </summary>
        private string currentSessionId;

        /// <summary>
        /// Timer for periodically refreshing the chat messages.
        /// </summary>
        private Timer chatRefreshTimer;

        /// <summary>
        /// The current list of chat messages for the session.
        /// </summary>
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
        /// Sets up the slideshow, quote rotation, welcome sound, chat session, and initial greeting.
        /// </summary>
        public WelcomeForm()
        {
            InitializeComponent();
            pictureBoxSlideshow.Image = Image.FromFile(hotelImages[currentImage]);
            lblQuote.Text = quotes[0];
            StartSlideshow();
            StartQuoteRotation();
            PlayWelcomeSound();
            currentSessionId = Guid.NewGuid().ToString(); // Create unique session
            StartChatRefresh();
            SendInitialGreeting();
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
        /// Handles the Admin Login button click event. Opens the admin login form and, if successful, the admin form.
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
        /// Starts the timer to periodically refresh the chat messages.
        /// </summary>
        private void StartChatRefresh()
        {
            chatRefreshTimer = new Timer();
            chatRefreshTimer.Interval = 3000; // Every 3 seconds
            chatRefreshTimer.Tick += (s, e) => RefreshChat();
            chatRefreshTimer.Start();
        }

        /// <summary>
        /// Refreshes the chat messages from the server and updates the display if there are new messages.
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
                // Do not show error for periodic refresh
            }
        }

        /// <summary>
        /// Updates the chat display in the rich text box with the current messages.
        /// </summary>
        private void UpdateChatDisplay()
        {
            richTextBox1.Clear();
            foreach (var msg in currentMessages)
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.SelectionLength = 0;

                // Different color for each side
                if (msg.IsFromAdmin)
                    richTextBox1.SelectionBackColor = System.Drawing.Color.LightBlue;
                else
                    richTextBox1.SelectionBackColor = System.Drawing.Color.LightGray;

                // Name + message
                string prefix = msg.IsFromAdmin ? "🛎️ Admin" : "👤 You";
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
        /// Sends a chat message from the guest to the server and refreshes the chat display.
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
                RefreshChat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send message: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sends the initial greeting message from the admin to the guest when the form loads.
        /// </summary>
        private void SendInitialGreeting()
        {
            try
            {
                var message = new ChatMessage
                {
                    SenderName = "Admin",
                    Message = "How can I help you?",
                    IsFromAdmin = true,
                    SessionId = currentSessionId
                };
                api.SendChatMessage(message);
                RefreshChat();
            }
            catch
            {
                // Do not disturb user if this fails
            }
        }

        /// <summary>
        /// Handles the Send button click event to send a chat message.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendChatMessage();
        }

        /// <summary>
        /// Handles the KeyPress event for the message textbox to send a message on Enter key.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The key press event arguments.</param>
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
