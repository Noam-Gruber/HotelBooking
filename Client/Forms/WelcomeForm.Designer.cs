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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSlideshow)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(173, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(473, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome to the hotel booking app!";
            // 
            // btnNewBooking
            // 
            this.btnNewBooking.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNewBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNewBooking.ForeColor = System.Drawing.Color.White;
            this.btnNewBooking.Location = new System.Drawing.Point(614, 382);
            this.btnNewBooking.Name = "btnNewBooking";
            this.btnNewBooking.Size = new System.Drawing.Size(174, 56);
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
            this.btnAdminLogin.Location = new System.Drawing.Point(12, 382);
            this.btnAdminLogin.Name = "btnAdminLogin";
            this.btnAdminLogin.Size = new System.Drawing.Size(169, 56);
            this.btnAdminLogin.TabIndex = 2;
            this.btnAdminLogin.Text = "🔑 Admin Login";
            this.btnAdminLogin.UseVisualStyleBackColor = false;
            this.btnAdminLogin.Click += new System.EventHandler(this.btnAdminLogin_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(-1, 1);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTip.Location = new System.Drawing.Point(327, 406);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(182, 13);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "Tip: Book directly for exclusive deals!";
            // 
            // pictureBoxSlideshow
            // 
            this.pictureBoxSlideshow.Location = new System.Drawing.Point(180, 80);
            this.pictureBoxSlideshow.Name = "pictureBoxSlideshow";
            this.pictureBoxSlideshow.Size = new System.Drawing.Size(453, 261);
            this.pictureBoxSlideshow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSlideshow.TabIndex = 5;
            this.pictureBoxSlideshow.TabStop = false;
            // 
            // lblQuote
            // 
            this.lblQuote.AutoSize = true;
            this.lblQuote.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblQuote.ForeColor = System.Drawing.Color.DimGray;
            this.lblQuote.Location = new System.Drawing.Point(180, 350);
            this.lblQuote.Name = "lblQuote";
            this.lblQuote.Size = new System.Drawing.Size(50, 20);
            this.lblQuote.TabIndex = 6;
            this.lblQuote.Text = "label1";
            this.lblQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // accentBar
            // 
            this.accentBar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.accentBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.accentBar.Location = new System.Drawing.Point(0, 0);
            this.accentBar.Name = "accentBar";
            this.accentBar.Size = new System.Drawing.Size(800, 5);
            this.accentBar.TabIndex = 7;
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.accentBar);
            this.Controls.Add(this.lblQuote);
            this.Controls.Add(this.pictureBoxSlideshow);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.btnAdminLogin);
            this.Controls.Add(this.btnNewBooking);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WelcomeForm";
            this.Text = "Hotel Booking System";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSlideshow)).EndInit();
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
    }
}