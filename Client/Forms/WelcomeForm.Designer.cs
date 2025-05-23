namespace Client.Forms
{
    partial class WelcomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNewBooking = new System.Windows.Forms.Button();
            this.btnAdminLogin = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.pictureBoxSlideshow = new System.Windows.Forms.PictureBox();
            this.lblQuote = new System.Windows.Forms.Label();
            this.accentBar = new System.Windows.Forms.Panel();
            this.groupBox_chat = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSlideshow)).BeginInit();
            this.groupBox_chat.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(260, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(691, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome to the hotel booking app!";
            // 
            // btnNewBooking
            // 
            this.btnNewBooking.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNewBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNewBooking.ForeColor = System.Drawing.Color.White;
            this.btnNewBooking.Location = new System.Drawing.Point(921, 617);
            this.btnNewBooking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewBooking.Name = "btnNewBooking";
            this.btnNewBooking.Size = new System.Drawing.Size(261, 86);
            this.btnNewBooking.TabIndex = 1;
            this.btnNewBooking.Text = "🛎️ New Booking";
            this.btnNewBooking.UseVisualStyleBackColor = false;
            this.btnNewBooking.Click += new System.EventHandler(this.btnNewBooking_Click);
            this.btnNewBooking.MouseEnter += new System.EventHandler(this.btnNewBooking_MouseEnter);
            this.btnNewBooking.MouseLeave += new System.EventHandler(this.btnNewBooking_MouseLeave);
            // 
            // btnAdminLogin
            // 
            this.btnAdminLogin.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAdminLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAdminLogin.ForeColor = System.Drawing.Color.White;
            this.btnAdminLogin.Location = new System.Drawing.Point(18, 617);
            this.btnAdminLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdminLogin.Name = "btnAdminLogin";
            this.btnAdminLogin.Size = new System.Drawing.Size(254, 86);
            this.btnAdminLogin.TabIndex = 2;
            this.btnAdminLogin.Text = "🔑 Admin Login";
            this.btnAdminLogin.UseVisualStyleBackColor = false;
            this.btnAdminLogin.Click += new System.EventHandler(this.btnAdminLogin_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(-2, 2);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(96, 98);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTip.Location = new System.Drawing.Point(494, 691);
            this.lblTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(264, 20);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "Tip: Book directly for exclusive deals!";
            // 
            // pictureBoxSlideshow
            // 
            this.pictureBoxSlideshow.Location = new System.Drawing.Point(270, 123);
            this.pictureBoxSlideshow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxSlideshow.Name = "pictureBoxSlideshow";
            this.pictureBoxSlideshow.Size = new System.Drawing.Size(680, 402);
            this.pictureBoxSlideshow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSlideshow.TabIndex = 5;
            this.pictureBoxSlideshow.TabStop = false;
            // 
            // lblQuote
            // 
            this.lblQuote.AutoSize = true;
            this.lblQuote.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblQuote.ForeColor = System.Drawing.Color.DimGray;
            this.lblQuote.Location = new System.Drawing.Point(265, 88);
            this.lblQuote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuote.Name = "lblQuote";
            this.lblQuote.Size = new System.Drawing.Size(139, 30);
            this.lblQuote.TabIndex = 6;
            this.lblQuote.Text = "--------------";
            this.lblQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // accentBar
            // 
            this.accentBar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.accentBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.accentBar.Location = new System.Drawing.Point(0, 0);
            this.accentBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.accentBar.Name = "accentBar";
            this.accentBar.Size = new System.Drawing.Size(1200, 8);
            this.accentBar.TabIndex = 7;
            // 
            // groupBox_chat
            // 
            this.groupBox_chat.Controls.Add(this.richTextBox1);
            this.groupBox_chat.Controls.Add(this.textBox_message);
            this.groupBox_chat.Controls.Add(this.buttonSend);
            this.groupBox_chat.Location = new System.Drawing.Point(289, 533);
            this.groupBox_chat.Name = "groupBox_chat";
            this.groupBox_chat.Size = new System.Drawing.Size(625, 148);
            this.groupBox_chat.TabIndex = 26;
            this.groupBox_chat.TabStop = false;
            this.groupBox_chat.Text = "Chat";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(296, 18);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(323, 124);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(17, 31);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(273, 26);
            this.textBox_message.TabIndex = 1;
            this.textBox_message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_message_KeyPress);
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.buttonSend.ForeColor = System.Drawing.Color.White;
            this.buttonSend.Location = new System.Drawing.Point(17, 63);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(273, 64);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1200, 729);
            this.Controls.Add(this.groupBox_chat);
            this.Controls.Add(this.accentBar);
            this.Controls.Add(this.lblQuote);
            this.Controls.Add(this.pictureBoxSlideshow);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.btnAdminLogin);
            this.Controls.Add(this.btnNewBooking);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WelcomeForm";
            this.Text = "Hotel Booking System";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSlideshow)).EndInit();
            this.groupBox_chat.ResumeLayout(false);
            this.groupBox_chat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNewBooking;
        private System.Windows.Forms.Button btnAdminLogin;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.PictureBox pictureBoxSlideshow;
        private System.Windows.Forms.Label lblQuote;
        private System.Windows.Forms.Panel accentBar;
        private System.Windows.Forms.GroupBox groupBox_chat;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Button buttonSend;
    }
}