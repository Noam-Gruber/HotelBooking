
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numGuests)).BeginInit();
            this.SuspendLayout();
            // 
            // txtGuestName
            // 
            this.txtGuestName.Location = new System.Drawing.Point(111, 10);
            this.txtGuestName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtGuestName.Name = "txtGuestName";
            this.txtGuestName.Size = new System.Drawing.Size(175, 20);
            this.txtGuestName.TabIndex = 1;
            this.txtGuestName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExpiryYear
            // 
            this.txtExpiryYear.Location = new System.Drawing.Point(111, 250);
            this.txtExpiryYear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExpiryYear.Name = "txtExpiryYear";
            this.txtExpiryYear.Size = new System.Drawing.Size(76, 20);
            this.txtExpiryYear.TabIndex = 9;
            this.txtExpiryYear.Text = "2025";
            this.txtExpiryYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExpiryMonth
            // 
            this.txtExpiryMonth.Location = new System.Drawing.Point(111, 224);
            this.txtExpiryMonth.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExpiryMonth.Name = "txtExpiryMonth";
            this.txtExpiryMonth.Size = new System.Drawing.Size(76, 20);
            this.txtExpiryMonth.TabIndex = 8;
            this.txtExpiryMonth.Text = "01";
            this.txtExpiryMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(111, 40);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(175, 20);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.Text = "example@example.com";
            this.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name👤:";
            // 
            // label_Email
            // 
            this.label_Email.AutoSize = true;
            this.label_Email.Location = new System.Drawing.Point(14, 42);
            this.label_Email.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Email.Name = "label_Email";
            this.label_Email.Size = new System.Drawing.Size(53, 13);
            this.label_Email.TabIndex = 9;
            this.label_Email.Text = "Email 📧 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Check‑In 📅:";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Location = new System.Drawing.Point(111, 73);
            this.dtpCheckIn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(175, 20);
            this.dtpCheckIn.TabIndex = 3;
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged);
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Location = new System.Drawing.Point(111, 103);
            this.dtpCheckOut.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(175, 20);
            this.dtpCheckOut.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Check‑Out 📅:";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Items.AddRange(new object[] {
            "Single",
            "Double",
            "Suite"});
            this.cmbRoomType.Location = new System.Drawing.Point(111, 136);
            this.cmbRoomType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(175, 21);
            this.cmbRoomType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 139);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Room Type 🏨 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 198);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Card Name 💳 :";
            // 
            // label_ExpiryMonth
            // 
            this.label_ExpiryMonth.AutoSize = true;
            this.label_ExpiryMonth.Location = new System.Drawing.Point(16, 227);
            this.label_ExpiryMonth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ExpiryMonth.Name = "label_ExpiryMonth";
            this.label_ExpiryMonth.Size = new System.Drawing.Size(71, 13);
            this.label_ExpiryMonth.TabIndex = 17;
            this.label_ExpiryMonth.Text = "Expiry Month:";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(111, 198);
            this.txtCardNumber.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(175, 20);
            this.txtCardNumber.TabIndex = 7;
            this.txtCardNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardNumber_KeyPress);
            // 
            // numGuests
            // 
            this.numGuests.Location = new System.Drawing.Point(111, 168);
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
            this.numGuests.Size = new System.Drawing.Size(57, 20);
            this.numGuests.TabIndex = 6;
            this.numGuests.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numGuests.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 170);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Number Guest 👥:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 311);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(266, 38);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "🛎️ Book";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 253);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Expiry Year:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(111, 276);
            this.txtId.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(76, 20);
            this.txtId.TabIndex = 10;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 279);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Lookup ID 🔍:";
            // 
            // BookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(297, 361);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BookingForm";
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label8;
    }
}

