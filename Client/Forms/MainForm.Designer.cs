
namespace Client
{
    partial class MainForm
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
            this.txtGuestName = new System.Windows.Forms.TextBox();
            this.txtExpiryYear = new System.Windows.Forms.TextBox();
            this.txtExpiryMonth = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_Email = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_ExpiryMonth = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.numGuests = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).BeginInit();
            this.SuspendLayout();
            // 
            // txtGuestName
            // 
            this.txtGuestName.Location = new System.Drawing.Point(124, 12);
            this.txtGuestName.Name = "txtGuestName";
            this.txtGuestName.Size = new System.Drawing.Size(100, 22);
            this.txtGuestName.TabIndex = 0;
            // 
            // txtExpiryYear
            // 
            this.txtExpiryYear.Location = new System.Drawing.Point(124, 316);
            this.txtExpiryYear.Name = "txtExpiryYear";
            this.txtExpiryYear.Size = new System.Drawing.Size(100, 22);
            this.txtExpiryYear.TabIndex = 4;
            // 
            // txtExpiryMonth
            // 
            this.txtExpiryMonth.Location = new System.Drawing.Point(124, 284);
            this.txtExpiryMonth.Name = "txtExpiryMonth";
            this.txtExpiryMonth.Size = new System.Drawing.Size(100, 22);
            this.txtExpiryMonth.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(124, 49);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 22);
            this.txtEmail.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name:";
            // 
            // label_Email
            // 
            this.label_Email.AutoSize = true;
            this.label_Email.Location = new System.Drawing.Point(19, 52);
            this.label_Email.Name = "label_Email";
            this.label_Email.Size = new System.Drawing.Size(44, 16);
            this.label_Email.TabIndex = 9;
            this.label_Email.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Check‑In:";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Location = new System.Drawing.Point(124, 90);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(200, 22);
            this.dtpCheckIn.TabIndex = 11;
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Location = new System.Drawing.Point(124, 126);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(200, 22);
            this.dtpCheckOut.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Check‑Out:";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Location = new System.Drawing.Point(124, 168);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(121, 24);
            this.cmbRoomType.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Room Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Card Name:";
            // 
            // label_ExpiryMonth
            // 
            this.label_ExpiryMonth.AutoSize = true;
            this.label_ExpiryMonth.Location = new System.Drawing.Point(19, 287);
            this.label_ExpiryMonth.Name = "label_ExpiryMonth";
            this.label_ExpiryMonth.Size = new System.Drawing.Size(86, 16);
            this.label_ExpiryMonth.TabIndex = 17;
            this.label_ExpiryMonth.Text = "Expiry Month:";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(124, 252);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(100, 22);
            this.txtCardNumber.TabIndex = 18;
            // 
            // numGuests
            // 
            this.numGuests.Location = new System.Drawing.Point(124, 207);
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
            this.numGuests.Size = new System.Drawing.Size(76, 22);
            this.numGuests.TabIndex = 19;
            this.numGuests.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numGuests.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Number Guest:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(86, 382);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Book";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(187, 382);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 22;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 319);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Expiry Year:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(124, 348);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(100, 22);
            this.txtId.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 16);
            this.label8.TabIndex = 25;
            this.label8.Text = "Lookup ID:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numGuests);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.label_ExpiryMonth);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbRoomType);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_Email);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtExpiryMonth);
            this.Controls.Add(this.txtExpiryYear);
            this.Controls.Add(this.txtGuestName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "HotelBookingHub Client";
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGuestName;
        private System.Windows.Forms.TextBox txtExpiryYear;
        private System.Windows.Forms.TextBox txtExpiryMonth;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_ExpiryMonth;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.NumericUpDown numGuests;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label8;
    }
}

