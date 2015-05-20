namespace Client
{
    partial class ClientConnectionDialog
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
            this.labelServerIP = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelUserNameValidationMessage = new System.Windows.Forms.Label();
            this.labelServerIpValidationMessage = new System.Windows.Forms.Label();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.checkBoxEnableEncryption = new System.Windows.Forms.CheckBox();
            this.groupBoxEncryption = new System.Windows.Forms.GroupBox();
            this.richTextBoxEncryptionDescription = new System.Windows.Forms.RichTextBox();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.groupBoxEncryption.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelServerIP
            // 
            this.labelServerIP.Location = new System.Drawing.Point(6, 16);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(63, 20);
            this.labelServerIP.TabIndex = 0;
            this.labelServerIP.Text = "Server IP:";
            this.labelServerIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelUserName
            // 
            this.labelUserName.Location = new System.Drawing.Point(6, 65);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(63, 13);
            this.labelUserName.TabIndex = 1;
            this.labelUserName.Text = "User Name:";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(75, 16);
            this.textBoxServerIP.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(107, 20);
            this.textBoxServerIP.TabIndex = 2;
            this.textBoxServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(75, 62);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(141, 20);
            this.textBoxUserName.TabIndex = 3;
            this.textBoxUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonConnect
            // 
            this.buttonConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.Location = new System.Drawing.Point(12, 317);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(222, 51);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            // 
            // labelUserNameValidationMessage
            // 
            this.labelUserNameValidationMessage.ForeColor = System.Drawing.Color.Red;
            this.labelUserNameValidationMessage.Location = new System.Drawing.Point(75, 85);
            this.labelUserNameValidationMessage.Name = "labelUserNameValidationMessage";
            this.labelUserNameValidationMessage.Size = new System.Drawing.Size(141, 20);
            this.labelUserNameValidationMessage.TabIndex = 6;
            this.labelUserNameValidationMessage.Text = "Enter a username.";
            this.labelUserNameValidationMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelServerIpValidationMessage
            // 
            this.labelServerIpValidationMessage.ForeColor = System.Drawing.Color.Red;
            this.labelServerIpValidationMessage.Location = new System.Drawing.Point(75, 39);
            this.labelServerIpValidationMessage.Name = "labelServerIpValidationMessage";
            this.labelServerIpValidationMessage.Size = new System.Drawing.Size(141, 20);
            this.labelServerIpValidationMessage.TabIndex = 7;
            this.labelServerIpValidationMessage.Text = "Invalid IP address.";
            this.labelServerIpValidationMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDefaults.Location = new System.Drawing.Point(12, 128);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(222, 24);
            this.buttonDefaults.TabIndex = 8;
            this.buttonDefaults.Text = "Apply Defaults";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.buttonDefaults_Click);
            // 
            // checkBoxEnableEncryption
            // 
            this.checkBoxEnableEncryption.AutoSize = true;
            this.checkBoxEnableEncryption.Location = new System.Drawing.Point(78, 131);
            this.checkBoxEnableEncryption.Name = "checkBoxEnableEncryption";
            this.checkBoxEnableEncryption.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnableEncryption.TabIndex = 9;
            this.checkBoxEnableEncryption.Text = "Enabled";
            this.checkBoxEnableEncryption.UseVisualStyleBackColor = true;
            this.checkBoxEnableEncryption.CheckedChanged += new System.EventHandler(this.checkBoxEnableEncryption_CheckedChanged);
            // 
            // groupBoxEncryption
            // 
            this.groupBoxEncryption.Controls.Add(this.richTextBoxEncryptionDescription);
            this.groupBoxEncryption.Controls.Add(this.checkBoxEnableEncryption);
            this.groupBoxEncryption.Location = new System.Drawing.Point(12, 158);
            this.groupBoxEncryption.Name = "groupBoxEncryption";
            this.groupBoxEncryption.Size = new System.Drawing.Size(222, 153);
            this.groupBoxEncryption.TabIndex = 10;
            this.groupBoxEncryption.TabStop = false;
            this.groupBoxEncryption.Text = "Encryption";
            // 
            // richTextBoxEncryptionDescription
            // 
            this.richTextBoxEncryptionDescription.Location = new System.Drawing.Point(9, 20);
            this.richTextBoxEncryptionDescription.Name = "richTextBoxEncryptionDescription";
            this.richTextBoxEncryptionDescription.ReadOnly = true;
            this.richTextBoxEncryptionDescription.Size = new System.Drawing.Size(207, 105);
            this.richTextBoxEncryptionDescription.TabIndex = 10;
            this.richTextBoxEncryptionDescription.Text = "";
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.textBoxPort);
            this.groupBoxConnection.Controls.Add(this.labelServerIP);
            this.groupBoxConnection.Controls.Add(this.labelUserName);
            this.groupBoxConnection.Controls.Add(this.textBoxServerIP);
            this.groupBoxConnection.Controls.Add(this.labelServerIpValidationMessage);
            this.groupBoxConnection.Controls.Add(this.textBoxUserName);
            this.groupBoxConnection.Controls.Add(this.labelUserNameValidationMessage);
            this.groupBoxConnection.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(222, 110);
            this.groupBoxConnection.TabIndex = 11;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(182, 16);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.ReadOnly = true;
            this.textBoxPort.Size = new System.Drawing.Size(34, 20);
            this.textBoxPort.TabIndex = 8;
            this.textBoxPort.Text = ":1666";
            // 
            // ClientConnectionDialog
            // 
            this.AcceptButton = this.buttonConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 380);
            this.Controls.Add(this.groupBoxConnection);
            this.Controls.Add(this.groupBoxEncryption);
            this.Controls.Add(this.buttonDefaults);
            this.Controls.Add(this.buttonConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientConnectionDialog";
            this.Text = "Server Connection Assistant";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientConnectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.ClientConnectionDialog_Load);
            this.groupBoxEncryption.ResumeLayout(false);
            this.groupBoxEncryption.PerformLayout();
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelUserNameValidationMessage;
        private System.Windows.Forms.Label labelServerIpValidationMessage;
        private System.Windows.Forms.Button buttonDefaults;
        private System.Windows.Forms.CheckBox checkBoxEnableEncryption;
        private System.Windows.Forms.GroupBox groupBoxEncryption;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.RichTextBox richTextBoxEncryptionDescription;
        private System.Windows.Forms.TextBox textBoxPort;
    }
}