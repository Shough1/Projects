// /////////////////////////////////////////////////////////////////////////////
// CryptoChat Client
// Client.cs
//
// Provides a means to send messages to and receive messages from a CryptoChat Server.
// Messages have the option of being encrypted to prevent man-in-the-middle attacks.
//
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using CryptoLibrary;
using System.Collections.Generic;
using System.Collections;

namespace Client
{
    public partial class ClientForm : Form
    {
        #region Member Variables and Constructor

        //these are used to prevent cross-thread violations
        private delegate void delVoidVoid();
        private delegate void delVoidObject(object o);
        private delegate void delVoidSocket(Socket s);
        private delegate void delVoidString(string s);
        private delegate void delVoidStringColor(string s, Color c);

        private ClientConnectionDialog _connectionDialog = new ClientConnectionDialog();    //help get connection information from user
        private ConnectionHelper _connectionHelper = null;      //handles all regular socket communication (not the handshake)
        private const int PORT = 1666; //always use this port

        private DESCryptoServiceProvider _des = new DESCryptoServiceProvider(); //holds secret key and initialization vector
        private bool _encryptionEnabled = false;    //encryption is off by default
        
        private List<ClientInfoFrame> clients = new List<ClientInfoFrame>();  //hold information about clients currently connected to 
                                                                              //the server (includes this client when connected)

        public ClientForm()
        {
            InitializeComponent();

            _connectionDialog.StartPosition = FormStartPosition.CenterParent;
            
            //listViewConnectedClients.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            
            resetUiControls();
        }

        #endregion
                
        #region EventHandlers

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (_encryptionEnabled)
            {
                //encrypt the message before sending
                var messageFrame = new MessageFrame { Message = textBoxMessage.Text };
                var payload = CryptoHelper.Encrypt<MessageFrame>(messageFrame, _des);
                var cryptoFrame = new CryptoFrame { Payload = payload };
                _connectionHelper.Send<CryptoFrame>(cryptoFrame);
            }
            else
            {
                //send the message unencrypted
                _connectionHelper.Send<MessageFrame>(new MessageFrame { Message = textBoxMessage.Text });
            }

            textBoxMessage.Text = "";

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //check for manual disconnect first
            if (buttonConnect.Text != "Connect")
            {
                //postMessage("The client chose to manually disconnect.");
                disconnect("The client chose to manually disconnect.");
                return;
            }

            //client wants to connect
            if (_connectionDialog.ShowDialog() == DialogResult.OK)
            {
                //connect to the server
                _encryptionEnabled = _connectionDialog.EncryptionEnabled;

                buttonConnect.Enabled = false;
                labelConnectionStatus.Text = "Connecting...";
                labelConnectionStatus.ForeColor = Color.Black;

                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //postMessage(string.Format("Attempting to connect to {0} on port {1}...", _connectionDialog.ServerIP, PORT));
                Console.WriteLine(string.Format("Attempting to connect to {0} on port {1}...", _connectionDialog.ServerIP, PORT));

                socket.BeginConnect(_connectionDialog.ServerIP, PORT, socketBeginConnectCallback, socket);

            }

        }

        //only enable the send button if there is something prepared to be sent
        private void textBoxMessage_TextChanged(object sender, EventArgs e)
        {
            buttonSend.Enabled = textBoxMessage.Text.Length > 0;
        }

        private void buttonWarning_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Client names that appear with a red background are NOT using encryption. " +
                "Even if you are using encryption, messages sent between the server and the unencrypted " +
                "client will be vulnerable to man-in-the-middle attacks.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

        #region Handshake

        //carry out an encryption handshake
        private void handshakeThread(object socketObject)
        {
            var socket = socketObject as Socket;    //communicate with server with this
            object responseObject;                  //keep public key from server here

            if (!handshakePhase1_request(socket))
                return;

            if (!handshakePhase2_response(socket, out responseObject))
                return;

            if (!handshakePhase3_key(socket, responseObject))
                return;

            //in case the form is disposed, this will allow a graceful shutdown
            try
            {
                //at this point the handshake was successful
                handshakeSucceeded(socket);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        //called after a successful encryption handshake
        private void handshakeSucceeded(Socket socket)
        {
            if (InvokeRequired)
            {
                Invoke(new delVoidSocket(handshakeSucceeded), socket);
                return;
            }

            postMessage("Handshake succeeded.", Color.Blue);
            startCommunicating(socket);
        }

        //called if any stage of the handshake failed
        private void handshakeFailed(string reason)
        {
            //this method takes care of invoking itself on the form ui thread, if necessary
            if (InvokeRequired)
            {
                Invoke(new delVoidString(handshakeFailed), reason);
                return;
            }

            postMessage(string.Format("Handshake failed: {0}", reason), Color.Red);
            resetUiControls();
            return;
        }

        //phase 1 - request public key
        private bool handshakePhase1_request(Socket socket)
        {
            //prepare a request frame to initiate handshake with server
            var requestFrameData = ConnectionHelper.SerializeFrame<RequestFrame>(new RequestFrame());

            try
            {
                socket.Send(requestFrameData);
            }
            catch (SocketException se)
            {
                //in case the form is disposed, this will allow a graceful shutdown
                try
                {
                    postMessage("Failed to send request to server.", Color.Red);
                    handshakeFailed(se.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                
                return false;
            }

            postMessage("Client sent request. Waiting for reponse from server...", Color.Blue);
            return true;
        }

        //phase 2 - receive response
        private bool handshakePhase2_response(Socket socket, out object responseObject)
        {
            var responseFrameData = new byte[10000];    //catch received data here
            var byteCount = 0;                          //keep track of how many bytes were received
            var msResponseFrame = new MemoryStream();   //use this to handle fragments

            responseObject = null;

            socket.ReceiveTimeout = 3000; //we can't wait forever

            //although unlikely, a fragment could be received here

            do
            {
                try
                {
                    byteCount = socket.Receive(responseFrameData);
                }
                catch (SocketException se)
                {
                    //in case the form is disposed, this will allow a graceful shutdown
                    try
                    {
                        handshakeFailed(se.Message);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }

                    return false;
                }

                ConnectionHelper.UpdateReceiveStream(msResponseFrame, responseFrameData, byteCount);

                try
                {
                    responseObject = new BinaryFormatter().Deserialize(msResponseFrame);
                }
                catch (SerializationException)
                {
                    postMessage("Received fragment. Waiting for the rest of the data...", Color.Blue);
                    msResponseFrame.Seek(0, SeekOrigin.Begin);
                }
            }
            while (responseObject == null);

            //abandon this handshake if we do not get a response frame here
            if (!(responseObject is ResponseFrame))
            {
                try
                {
                    handshakeFailed("Expecting response frame; did not receive response frame.");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                return false;
            }

            //in case the form is disposed, this will allow a graceful shutdown
            try
            {
                postMessage("Client received response from server.", Color.Blue);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return true;
        }

        //phase 3 - send symmetric key
        private bool handshakePhase3_key(Socket socket, object responseObject)
        {
            var publicKey = ((ResponseFrame)responseObject).PublicKey;      //extract the server's public key string
            var keyFrame = CryptoHelper.BuildKeyFrame(publicKey, _des);     //construct a key frame that contains the client's secret key
                                                                            //encrypted with the server's public key
            
            var keyFrameData = ConnectionHelper.SerializeFrame<KeyFrame>(keyFrame);     //prepare the key frame for transport

            try
            {
                socket.Send(keyFrameData);
            }
            catch (SocketException se)
            {
                try
                {
                    postMessage("Failed to send key to server.", Color.Red);
                    handshakeFailed(se.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                return false;
            }

            //in case the form is disposed, this will allow a graceful shutdown
            try
            {
                postMessage("Sent the server the private key.", Color.Blue);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return true;
        }

        #endregion

        #region Socket Connection

        //called after receiving a response from the server
        private void socketBeginConnectCallback(IAsyncResult asyncResult)
        {
            var socket = (Socket)asyncResult.AsyncState;

            try
            {
                socket.EndConnect(asyncResult);
            }
            catch (SocketException se)
            {
                socketEndConnectFailed(se.Message);
                return;
            }

            socketEndConnectSucceeded(socket);
        }

        //called if a connection could not be made with the server
        private void socketEndConnectFailed(string message)
        {
            //this method takes care of invoking itself on the form ui thread, if necessary
            if (InvokeRequired)
            {
                Invoke(new delVoidString(socketEndConnectFailed), message);
                return;
            }

            postMessage(message, Color.Red);

            //let user try to connect again
            buttonConnect.Enabled = true;
            labelConnectionStatus.Text = "Disconnected";
            labelConnectionStatus.ForeColor = Color.Red;
        }

        //called after a successful connection to the server
        private void socketEndConnectSucceeded(Socket socket)
        {
            //this method takes care of invoking itself on the form ui thread, if necessary
            if (InvokeRequired)
            {
                Invoke(new delVoidSocket(socketEndConnectSucceeded), socket);
                return;
            }

            postMessage(string.Format("The client has established a connection with the server ({0})", socket.RemoteEndPoint.ToString()), Color.Green);

            if (_encryptionEnabled)
            {
                postMessage("Encryption enabled; starting handshake...", Color.Blue);
                labelConnectionStatus.Text = "Handshaking...";

                //start the encryption 3-way handshake
                var thread = new Thread(handshakeThread);
                thread.IsBackground = true;
                thread.Start(socket);
            }
            else
            {
                postMessage("Warning: Encryption disabled; your messages may be intercepted during transit.", Color.Red);

                startCommunicating(socket);
            }
        }

        //process data received from the server
        private void dataHandlerCallback(object data)
        {
            //this method takes care of invoking itself on the form ui thread, if necessary
            if (InvokeRequired)
            {
                Invoke(new delVoidObject(dataHandlerCallback), data);
                return;
            }

            //intermediate decryption processing
            if (_encryptionEnabled)
            {
                var strangerDetected = !(data is CryptoFrame);

                if (strangerDetected)
                {
                    disconnect("Expecting CryptoFrame. Did not get CryptoFrame. Buh-bye.");
                    return;
                }

                //got a cryptoframe -> unpack payload
                var payload = (data as CryptoFrame).Payload;
                //reuse 'data' to hold deserialized object, so regular rtti code can work on it
                data = CryptoHelper.DecryptToObject(payload, _des);
            }

            if (data is MessageFrame) //someone sent us a message! (actually, it could just be an echo of our own message)
            {
                var frame = ((MessageFrame)data);
                var ownMessage = frame.Sender == _connectionDialog.UserName;
                
                postMessage(
                    string.Format("[{2}] {0}: {1}", frame.Sender, frame.Message, frame.Datetime),
                    ownMessage ? Color.Purple : Color.CadetBlue); //distinguish between our messages and other clients' messages
            }
            else if (data is ClientInfoFrame) //someone new has connected to the server (or disconnected)
            {
                var clientInfoFrame = data as ClientInfoFrame;

                //postMessage(string.Format("Received client info for {0}.", clientInfoFrame.Name));
                Console.WriteLine(string.Format("Received client info for {0}.", clientInfoFrame.Name));

                if (clientInfoFrame.nameTaken)
                {
                    var message = string.Format("The name '{0}' was already taken.", clientInfoFrame.Name);
                    disconnect(message);
                    postMessage(message, Color.Red);
                    //buttonConnect.PerformClick(); //this might be annoying
                }
                else if (clientInfoFrame.Joining) //this client has joined the server
                {
                    //server will take care of ensuring no duplicate user names
                    clients.Add(clientInfoFrame);

                    postMessage(string.Format("{0} has joined the chat room.", clientInfoFrame.Name), Color.Green);
                }
                else //this client has left the server
                {
                    var clientToRemove = clients.Find(c => c.Name == clientInfoFrame.Name);

                    if (clientToRemove != default(ClientInfoFrame))
                    {
                        clients.Remove(clientToRemove);
                        postMessage(string.Format("{0} has left the chat room.", clientInfoFrame.Name));
                    }
                    else
                    {
                        postMessage(string.Format("client was told that {0} left, but {0} was not here.", clientInfoFrame.Name), Color.Red);
                    }
                }

                updateClientList();

            }
            else
            {
                disconnect("Client: Received unknown frame type. Disconnecting from server.");
                return;
            }

        }

        //make client list reflect connected client data
        private void updateClientList()
        {
            listViewConnectedClients.Items.Clear();

            foreach (var client in clients)
            {
                var listViewItem = new ListViewItem(client.Name);

                //display "our" name in a special color
                if (client.Name == _connectionDialog.UserName)
                    listViewItem.ForeColor = Color.Purple;

                //highlight clients that are not using encryption
                if (!client.UsingEncryption)
                    listViewItem.BackColor = Color.LightCoral;
                
                listViewConnectedClients.Items.Add(listViewItem);
            }
        }

        #endregion

        #region Helper Methods

        //posts a plain, black message
        private void postMessage(string message)
        {
            postMessage(message, Color.Black);
        }

        //posts a message in a specified color
        private void postMessage(string message, Color color)
        {
            //this method takes care of invoking itself on the form ui thread, if necessary
            if (InvokeRequired)
            {
                Invoke(new delVoidStringColor(postMessage), message, color);
                return;
            }

            richTextBoxMessages.AppendText(string.Format("{0}\r\n", message), color);

            //emoticon fun: removed because it crashes sometimes

            //var ht = new Hashtable(1);
            //var emote = ":)";

            //ht.Add(emote, Properties.Resources.tentacles);

            //var ind = richTextBoxMessages.Text.IndexOf(emote);

            //if (ind != -1)
            //{
            //    richTextBoxMessages.Select(ind, emote.Length);
            //    Clipboard.SetImage((Image)ht[emote]);
            //    richTextBoxMessages.Paste();
            //}
            
            //automatically scroll to the bottom of the textbox
            richTextBoxMessages.SelectionStart = richTextBoxMessages.Text.Length;
            richTextBoxMessages.ScrollToCaret();
        }

        //restores client to a disconnected state
        private void resetUiControls()
        {
            labelUserName.Text = "";
            buttonConnect.Text = "Connect";
            buttonConnect.Enabled = true;
            labelEncryptionStatus.Visible = false;
            labelConnectionStatus.Text = "Disconnected";
            labelConnectionStatus.ForeColor = Color.Red;

            textBoxMessage.Enabled = false;
            textBoxMessage.Text = "";
            buttonSend.Enabled = false;

            clients.Clear();
            updateClientList();

        }

        //disconnect the client from the server
        private void disconnect(string reason = "Disconnected (unknown reason)")
        {
            //this method takes care of invoking itself on the form ui thread, if necessary
            if (InvokeRequired)
            {
                Invoke(new delVoidString(disconnect), reason);
                return;
            }

            if (_connectionHelper != null)
            {
                _connectionHelper.Disconnect();
                
                postMessage(string.Format("Disconnected: {0}", reason));
                Console.WriteLine(string.Format("Disconnected: {0}", reason));

                resetUiControls();
            }
        }

        //starts communication with the server
        private void startCommunicating(Socket socket)
        {
            _connectionHelper = new ConnectionHelper(socket);

            //setup callback methods for _connection
            _connectionHelper.DataHandler += dataHandlerCallback;
            _connectionHelper.OnDisconnect += disconnect;

            //send a client info frame
            if (_encryptionEnabled)
            {
                var clientInfoFrame = new ClientInfoFrame { Name = _connectionDialog.UserName };
                var payload = CryptoHelper.Encrypt<ClientInfoFrame>(clientInfoFrame, _des);
                var cryptoFrame = new CryptoFrame { Payload = payload };
                _connectionHelper.Send<CryptoFrame>(cryptoFrame);
            }
            else
            {
                _connectionHelper.Send<ClientInfoFrame>(new ClientInfoFrame { Name = _connectionDialog.UserName });
            }

            //enable ui controls
            labelConnectionStatus.Text = "Connected";
            labelConnectionStatus.ForeColor = Color.Green;
            labelEncryptionStatus.Visible = true;
            labelEncryptionStatus.Text = string.Format("Encryption {0}", _encryptionEnabled ? "Enabled" : "Disabled");
            labelEncryptionStatus.ForeColor = _encryptionEnabled ? Color.Blue : Color.Red;
            buttonConnect.Enabled = true;
            buttonConnect.Text = "Disconnect";
            richTextBoxMessages.Enabled = true;
            textBoxMessage.Enabled = true;
            labelUserName.Text = string.Format("Hello, {0}", _connectionDialog.UserName);
            textBoxMessage.Focus();

        }

        #endregion

    }

    #region Extensions

    //retrieved from http://stackoverflow.com/a/1926822/1455558
    public static class RichTextBoxExtensions
    {
        //allow colored text to be easily appended to a RichTextBox
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            //positino cursor at the end of the rich text box
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            //write text in the specified color
            box.SelectionColor = color;
            box.AppendText(text);

            //restore the color of rich text box 
            box.SelectionColor = box.ForeColor;
        }
    }

    #endregion
}
