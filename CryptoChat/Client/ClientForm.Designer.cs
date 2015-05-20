namespace Client
{
    partial class ClientForm
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
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelConnectionStatus = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelEncryptionStatus = new System.Windows.Forms.Label();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.labelConnectedClients = new System.Windows.Forms.Label();
            this.labelMessages = new System.Windows.Forms.Label();
            this.labelHorizontalLine = new System.Windows.Forms.Label();
            this.listViewConnectedClients = new System.Windows.Forms.ListView();
            this.columnDummy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonWarning = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessage.Location = new System.Drawing.Point(137, 295);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(365, 29);
            this.textBoxMessage.TabIndex = 1;
            this.textBoxMessage.TextChanged += new System.EventHandler(this.textBoxMessage_TextChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(505, 295);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(54, 29);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelConnectionStatus
            // 
            this.labelConnectionStatus.Location = new System.Drawing.Point(93, 12);
            this.labelConnectionStatus.Name = "labelConnectionStatus";
            this.labelConnectionStatus.Size = new System.Drawing.Size(100, 23);
            this.labelConnectionStatus.TabIndex = 7;
            this.labelConnectionStatus.Text = "connection status";
            this.labelConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUserName
            // 
            this.labelUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUserName.Location = new System.Drawing.Point(309, 12);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(250, 23);
            this.labelUserName.TabIndex = 8;
            this.labelUserName.Text = "Hello, username";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEncryptionStatus
            // 
            this.labelEncryptionStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelEncryptionStatus.Location = new System.Drawing.Point(199, 12);
            this.labelEncryptionStatus.Name = "labelEncryptionStatus";
            this.labelEncryptionStatus.Size = new System.Drawing.Size(104, 23);
            this.labelEncryptionStatus.TabIndex = 9;
            this.labelEncryptionStatus.Text = "encryption status";
            this.labelEncryptionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessages.Location = new System.Drawing.Point(137, 82);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.Size = new System.Drawing.Size(422, 207);
            this.richTextBoxMessages.TabIndex = 10;
            this.richTextBoxMessages.Text = "";
            // 
            // labelConnectedClients
            // 
            this.labelConnectedClients.Location = new System.Drawing.Point(12, 56);
            this.labelConnectedClients.Name = "labelConnectedClients";
            this.labelConnectedClients.Size = new System.Drawing.Size(119, 23);
            this.labelConnectedClients.TabIndex = 12;
            this.labelConnectedClients.Text = "Connected Clients";
            this.labelConnectedClients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMessages
            // 
            this.labelMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMessages.Location = new System.Drawing.Point(137, 56);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Size = new System.Drawing.Size(422, 23);
            this.labelMessages.TabIndex = 13;
            this.labelMessages.Text = "Messages";
            this.labelMessages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHorizontalLine
            // 
            this.labelHorizontalLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHorizontalLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelHorizontalLine.Location = new System.Drawing.Point(12, 46);
            this.labelHorizontalLine.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.labelHorizontalLine.Name = "labelHorizontalLine";
            this.labelHorizontalLine.Size = new System.Drawing.Size(547, 2);
            this.labelHorizontalLine.TabIndex = 14;
            // 
            // listViewConnectedClients
            // 
            this.listViewConnectedClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDummy});
            this.listViewConnectedClients.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewConnectedClients.Location = new System.Drawing.Point(12, 82);
            this.listViewConnectedClients.Name = "listViewConnectedClients";
            this.listViewConnectedClients.Size = new System.Drawing.Size(119, 117);
            this.listViewConnectedClients.TabIndex = 15;
            this.listViewConnectedClients.UseCompatibleStateImageBehavior = false;
            this.listViewConnectedClients.View = System.Windows.Forms.View.List;
            // 
            // columnDummy
            // 
            this.columnDummy.Text = "columnDummy";
            this.columnDummy.Width = 62;
            // 
            // buttonWarning
            // 
            this.buttonWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWarning.BackgroundImage = global::Client.Properties.Resources.emblem_danger;
            this.buttonWarning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonWarning.Location = new System.Drawing.Point(12, 205);
            this.buttonWarning.Name = "buttonWarning";
            this.buttonWarning.Size = new System.Drawing.Size(119, 119);
            this.buttonWarning.TabIndex = 16;
            this.buttonWarning.UseVisualStyleBackColor = true;
            this.buttonWarning.Click += new System.EventHandler(this.buttonWarning_Click);
            // 
            // ClientForm
            // 
            this.AcceptButton = this.buttonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 335);
            this.Controls.Add(this.buttonWarning);
            this.Controls.Add(this.listViewConnectedClients);
            this.Controls.Add(this.labelHorizontalLine);
            this.Controls.Add(this.labelMessages);
            this.Controls.Add(this.labelConnectedClients);
            this.Controls.Add(this.richTextBoxMessages);
            this.Controls.Add(this.labelEncryptionStatus);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.labelConnectionStatus);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxMessage);
            this.MinimumSize = new System.Drawing.Size(587, 373);
            this.Name = "ClientForm";
            this.Text = "CryptoChat Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelConnectionStatus;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelEncryptionStatus;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.Label labelConnectedClients;
        private System.Windows.Forms.Label labelMessages;
        private System.Windows.Forms.Label labelHorizontalLine;
        private System.Windows.Forms.ListView listViewConnectedClients;
        private System.Windows.Forms.Button buttonWarning;
        private System.Windows.Forms.ColumnHeader columnDummy;

    }
}

