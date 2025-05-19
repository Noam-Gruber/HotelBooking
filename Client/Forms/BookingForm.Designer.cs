
namespace Client
{
    partial class BookingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numGuests = new System.Windows.Forms.NumericUpDown();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.label_ExpiryMonth = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label_Email = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtExpiryMonth = new System.Windows.Forms.TextBox();
            this.txtExpiryYear = new System.Windows.Forms.TextBox();
            this.txtGuestName = new System.Windows.Forms.TextBox();
            this.accentBar = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(12, 405);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(342, 38);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "🛎️ Book";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.numGuests);
            this.panel1.Controls.Add(this.txtCardNumber);
            this.panel1.Controls.Add(this.label_ExpiryMonth);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbRoomType);
            this.panel1.Controls.Add(this.dtpCheckOut);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpCheckIn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label_Email);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtExpiryMonth);
            this.panel1.Controls.Add(this.txtExpiryYear);
            this.panel1.Controls.Add(this.txtGuestName);
            this.panel1.Location = new System.Drawing.Point(12, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 288);
            this.panel1.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(14, 254);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 19);
            this.label6.TabIndex = 44;
            this.label6.Text = "Expiry Year:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(14, 164);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 19);
            this.label7.TabIndex = 43;
            this.label7.Text = "Number Guest 👥:";
            // 
            // numGuests
            // 
            this.numGuests.Location = new System.Drawing.Point(143, 164);
            this.numGuests.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.numGuests.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numGuests.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGuests.Name = "numGuests";
            this.numGuests.Size = new System.Drawing.Size(175, 20);
            this.numGuests.TabIndex = 31;
            this.numGuests.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numGuests.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGuests.ValueChanged += new System.EventHandler(this.numGuests_ValueChanged);
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(143, 194);
            this.txtCardNumber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(175, 20);
            this.txtCardNumber.TabIndex = 32;
            this.txtCardNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ExpiryMonth
            // 
            this.label_ExpiryMonth.AutoSize = true;
            this.label_ExpiryMonth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label_ExpiryMonth.ForeColor = System.Drawing.Color.Navy;
            this.label_ExpiryMonth.Location = new System.Drawing.Point(14, 224);
            this.label_ExpiryMonth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ExpiryMonth.Name = "label_ExpiryMonth";
            this.label_ExpiryMonth.Size = new System.Drawing.Size(94, 19);
            this.label_ExpiryMonth.TabIndex = 42;
            this.label_ExpiryMonth.Text = "Expiry Month:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(14, 194);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 19);
            this.label5.TabIndex = 41;
            this.label5.Text = "Card Name 💳 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(14, 134);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 19);
            this.label4.TabIndex = 40;
            this.label4.Text = "Room Type 🏨 :";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Items.AddRange(new object[] {
            "Single",
            "Double",
            "Suite"});
            this.cmbRoomType.Location = new System.Drawing.Point(143, 134);
            this.cmbRoomType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(175, 21);
            this.cmbRoomType.TabIndex = 30;
            this.cmbRoomType.TextChanged += new System.EventHandler(this.cmbRoomType_TextChanged);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Location = new System.Drawing.Point(143, 104);
            this.dtpCheckOut.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(175, 20);
            this.dtpCheckOut.TabIndex = 29;
            this.dtpCheckOut.ValueChanged += new System.EventHandler(this.dtpCheckOut_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(14, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 19);
            this.label3.TabIndex = 39;
            this.label3.Text = "Check‑Out 📅:";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Location = new System.Drawing.Point(143, 74);
            this.dtpCheckIn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(175, 20);
            this.dtpCheckIn.TabIndex = 28;
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(14, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 37;
            this.label2.Text = "Check‑In 📅:";
            // 
            // label_Email
            // 
            this.label_Email.AutoSize = true;
            this.label_Email.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.label_Email.ForeColor = System.Drawing.Color.Navy;
            this.label_Email.Location = new System.Drawing.Point(14, 44);
            this.label_Email.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Email.Name = "label_Email";
            this.label_Email.Size = new System.Drawing.Size(71, 19);
            this.label_Email.TabIndex = 36;
            this.label_Email.Text = "Email 📧 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 19);
            this.label1.TabIndex = 33;
            this.label1.Text = "Name👤:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(143, 44);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(175, 20);
            this.txtEmail.TabIndex = 27;
            this.txtEmail.Text = "example@example.com";
            this.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExpiryMonth
            // 
            this.txtExpiryMonth.Location = new System.Drawing.Point(143, 224);
            this.txtExpiryMonth.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExpiryMonth.Name = "txtExpiryMonth";
            this.txtExpiryMonth.Size = new System.Drawing.Size(175, 20);
            this.txtExpiryMonth.TabIndex = 34;
            this.txtExpiryMonth.Text = "01";
            this.txtExpiryMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExpiryYear
            // 
            this.txtExpiryYear.Location = new System.Drawing.Point(143, 254);
            this.txtExpiryYear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExpiryYear.Name = "txtExpiryYear";
            this.txtExpiryYear.Size = new System.Drawing.Size(175, 20);
            this.txtExpiryYear.TabIndex = 35;
            this.txtExpiryYear.Text = "2025";
            this.txtExpiryYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGuestName
            // 
            this.txtGuestName.Location = new System.Drawing.Point(143, 14);
            this.txtGuestName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtGuestName.Name = "txtGuestName";
            this.txtGuestName.Size = new System.Drawing.Size(175, 20);
            this.txtGuestName.TabIndex = 26;
            this.txtGuestName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGuestName.TextChanged += new System.EventHandler(this.txtGuestName_TextChanged);
            // 
            // accentBar
            // 
            this.accentBar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.accentBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.accentBar.Location = new System.Drawing.Point(0, 0);
            this.accentBar.Name = "accentBar";
            this.accentBar.Size = new System.Drawing.Size(366, 5);
            this.accentBar.TabIndex = 23;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 3);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 24;
            this.pictureBoxLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(116, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(124, 37);
            this.lblTitle.TabIndex = 25;
            this.lblTitle.Text = "Booking";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblSummary.ForeColor = System.Drawing.Color.Teal;
            this.lblSummary.Location = new System.Drawing.Point(26, 369);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(22, 15);
            this.lblSummary.TabIndex = 26;
            this.lblSummary.Text = "---";
            // 
            // BookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(366, 463);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.accentBar);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BookingForm";
            this.Text = "Hotel Booking Hub";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numGuests;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label label_ExpiryMonth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_Email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtExpiryMonth;
        private System.Windows.Forms.TextBox txtExpiryYear;
        private System.Windows.Forms.TextBox txtGuestName;
        private System.Windows.Forms.Panel accentBar;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSummary;
    }
}

