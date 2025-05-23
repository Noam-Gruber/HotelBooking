namespace Client.Forms
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.accentBar = new System.Windows.Forms.Panel();
            this.groupBox_chat = new System.Windows.Forms.GroupBox();
            this.labelSelectClient = new System.Windows.Forms.Label();
            this.comboBoxClients = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.btnDeleteOldChats = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox_chat.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(16, 106);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.Size = new System.Drawing.Size(1270, 333);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGridView_RowPrePaint);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(546, 32);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(178, 54);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(105, 37);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(268, 39);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(384, 32);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(153, 54);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(96, 98);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // accentBar
            // 
            this.accentBar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.accentBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.accentBar.Location = new System.Drawing.Point(0, 0);
            this.accentBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.accentBar.Name = "accentBar";
            this.accentBar.Size = new System.Drawing.Size(1299, 8);
            this.accentBar.TabIndex = 24;
            // 
            // groupBox_chat
            // 
            this.groupBox_chat.Controls.Add(this.btnDeleteOldChats);
            this.groupBox_chat.Controls.Add(this.labelSelectClient);
            this.groupBox_chat.Controls.Add(this.comboBoxClients);
            this.groupBox_chat.Controls.Add(this.richTextBox1);
            this.groupBox_chat.Controls.Add(this.textBox_message);
            this.groupBox_chat.Controls.Add(this.buttonSend);
            this.groupBox_chat.Location = new System.Drawing.Point(16, 445);
            this.groupBox_chat.Name = "groupBox_chat";
            this.groupBox_chat.Size = new System.Drawing.Size(958, 206);
            this.groupBox_chat.TabIndex = 25;
            this.groupBox_chat.TabStop = false;
            this.groupBox_chat.Text = "Chat";
            // 
            // labelSelectClient
            // 
            this.labelSelectClient.AutoSize = true;
            this.labelSelectClient.Location = new System.Drawing.Point(6, 25);
            this.labelSelectClient.Name = "labelSelectClient";
            this.labelSelectClient.Size = new System.Drawing.Size(102, 20);
            this.labelSelectClient.TabIndex = 4;
            this.labelSelectClient.Text = "Select Client:";
            // 
            // comboBoxClients
            // 
            this.comboBoxClients.FormattingEnabled = true;
            this.comboBoxClients.Location = new System.Drawing.Point(6, 49);
            this.comboBoxClients.Name = "comboBoxClients";
            this.comboBoxClients.Size = new System.Drawing.Size(344, 28);
            this.comboBoxClients.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(356, 25);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(596, 175);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(6, 92);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(344, 26);
            this.textBox_message.TabIndex = 1;
            this.textBox_message.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_message_KeyPress);
            // 
            // buttonSend
            // 
            this.buttonSend.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.buttonSend.ForeColor = System.Drawing.Color.White;
            this.buttonSend.Location = new System.Drawing.Point(6, 133);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(344, 55);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // btnDeleteOldChats
            // 
            this.btnDeleteOldChats.Location = new System.Drawing.Point(178, 13);
            this.btnDeleteOldChats.Name = "btnDeleteOldChats";
            this.btnDeleteOldChats.Size = new System.Drawing.Size(160, 32);
            this.btnDeleteOldChats.TabIndex = 5;
            this.btnDeleteOldChats.Text = "Delete Old Chats";
            this.btnDeleteOldChats.UseVisualStyleBackColor = true;
            this.btnDeleteOldChats.Click += new System.EventHandler(this.btnDeleteOldChats_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1299, 663);
            this.Controls.Add(this.groupBox_chat);
            this.Controls.Add(this.accentBar);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminForm";
            this.Text = "Admin Window";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox_chat.ResumeLayout(false);
            this.groupBox_chat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel accentBar;
        private System.Windows.Forms.GroupBox groupBox_chat;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ComboBox comboBoxClients;
        private System.Windows.Forms.Label labelSelectClient;
        private System.Windows.Forms.Button btnDeleteOldChats;
    }
}