// /////////////////////////////////////////////////////////////////////////////
// CryptoChat Client Connection Dialog
// ClientConnectionDialog.cs
//
// This dialog assists the client with specifying information to use when
// connecting to the server. It performs the validation of this information
// as well.
//
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientConnectionDialog : Form
    {

        #region Member Variables and Constructors

        static private Random random = new Random();   //enable random name choosing

        public string ServerIP;         //client will attempt to connect to this
        public string UserName;         //this will identify this client's messages
        public bool EncryptionEnabled;  //this will determine if messages should be encrypted

        private List<string> usernames = null;  //store user name pool in this
        
        public ClientConnectionDialog()
        {
            InitializeComponent();

            //http://en.wikipedia.org/wiki/Firefly_%28TV_series%29
            usernames = new List<string>
            { 
                "Malcolm",
                "Zoe",
                "Wash",
                "Inara",
                "Jayne",
                "Kaylee",
                "Simon",
                "River",
                "Book",
            };

            //briefly explain the encryption capabilities offered by the client
            var encryptionMessage = "Enabling encryption will ensure that all traffic sent between " +
                "the client and the server will be kept safe from Man-in-the-middle attacks.\r\n\r\n" +
                "Check the \"Enabled\" option below to enable encryption.";

            richTextBoxEncryptionDescription.Text = encryptionMessage;

            //no encryption by default
            EncryptionEnabled = false;

        }

        #endregion

        #region Event Handlers

        private void ClientConnectionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            //validate server ip 

            IPAddress serverIP;
            var validServerIP = IPAddress.TryParse(textBoxServerIP.Text, out serverIP);

            if (!validServerIP)
            {
                labelServerIpValidationMessage.Visible = true;
                textBoxServerIP.Focus();
                textBoxServerIP.SelectAll();
                e.Cancel = true;
                return;
            }

            labelServerIpValidationMessage.Visible = false;

            //validate user name

            var userName = textBoxUserName.Text.Trim();     //extract "cleaned up" user name
            var validUserName = userName.Length > 0;

            if (!validUserName)
            {
                labelUserNameValidationMessage.Visible = true;
                textBoxUserName.Text = "";
                textBoxUserName.Focus();
                e.Cancel = true;
                return;
            }

            labelUserNameValidationMessage.Visible = false;     //user name is ok, so clear validation message 

            //validation passed

            ServerIP = serverIP.ToString();
            UserName = userName;
            DialogResult = DialogResult.OK;
        }

        private void ClientConnectionDialog_Load(object sender, EventArgs e)
        {
            //load the defaults
            buttonDefaults.PerformClick();
        }

        private void buttonDefaults_Click(object sender, EventArgs e)
        {
            textBoxServerIP.Text = "127.0.0.1";
            textBoxUserName.Text = NextUserName();

            //ensure any validation messages are hidden
            labelUserNameValidationMessage.Visible = false;
            labelServerIpValidationMessage.Visible = false;

            //this will prevent the dialog from closing (default action on button clicks)
            DialogResult = DialogResult.None;
        }

        private void checkBoxEnableEncryption_CheckedChanged(object sender, EventArgs e)
        {
            EncryptionEnabled = checkBoxEnableEncryption.Checked;
        }

        #endregion

        #region Helper Methods

        //returns a random Firefly character name that is different
        //from the currently entered name
        private string NextUserName()
        {
            var currentUserName = textBoxUserName.Text;
            var newUserName = currentUserName;

            while (newUserName == currentUserName)
                newUserName = usernames[random.Next(usernames.Count)];
            
            return newUserName;
        }

        #endregion

    }
}
