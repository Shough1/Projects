// /////////////////////////////////////////////////////////////////////////////
// CryptoChat Connected Client
// ConnectedClient.cs
// 
// Server to help connect multiple clients to one another and facilitate encryption chat.  
// 
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////
using CryptoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private List<ConnectedClient> clients = new List<ConnectedClient>();

        private string publicKey;
        private string privateKey;
        private string ip;

        List<ConnectionHelper> cClass = null;
        Socket listenSox = null;
        Socket connectSox = null;
        MessageFrame mf;

        private delegate void delVoidSocket(Socket sox);
        private delegate void delVoidConnectionHelper(ConnectionHelper ch);
        private delegate void delVoidStringConnectedClient(string s, ConnectedClient cc);

        private delegate void delVoidString(string msg);
        private delegate void delVoidVoid();
        private delegate void delVoidObject(object o);
        volatile bool NeedToEncrypt = false;
        private RSACryptoServiceProvider _rsa;

        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            _rsa = new RSACryptoServiceProvider(1024);
            listenSox = new Socket(AddressFamily.InterNetwork,
                                   SocketType.Stream,
                                   ProtocolType.Tcp);
            listenSox.Bind(new IPEndPoint(IPAddress.Any, 1666));
            listenSox.Listen(5);
            listenSox.BeginAccept(Accept, listenSox);
        }


        private void Accept(IAsyncResult ar)
        {
            Socket asock = (Socket)(ar.AsyncState);

            try
            {
                Socket connsok = asock.EndAccept(ar);
                Invoke(new delVoidSocket(Connections), connsok);

            }
            catch (SocketException e)
            {
                Invoke(new delVoidString(HandleError), e.Message);
            }
            catch(ObjectDisposedException ODE)
            {
                Console.WriteLine(ODE.Message + " : " + ODE.Source);
            }
        }

        private void Connections(Socket soc)
        {

            try
            {
                Text = "Connected";
                listenSox.BeginAccept(Accept, listenSox);
                ip = soc.RemoteEndPoint.ToString();
                var connectedClient = new ConnectedClient(new ConnectionHelper(soc),_rsa);
                connectedClient.receivedMessage = BroadcastMessage;
                connectedClient.clientsOnlineInfo = ClientInformation;
                connectedClient.clientHasDisconnected = ClientHasDisconnected;
                clients.Add(connectedClient);
                Text = clients.Count + " connected";
            }
            catch (SocketException err)
            {
                MessageBox.Show(err.Message, "Connections Error");
            }
        }

        private void InitalizeListView(string ip, ConnectedClient connectedClient)
        {
            var LI = ui_lstvw_connectedClients.Items.Add(ip);
            LI.SubItems.Add(connectedClient.Name);
            if (connectedClient.encryptedOn == false)
            {
                LI.SubItems.Add(connectedClient.Name + " is NOT sending encrypted data");
            }
            else
            {
                LI.SubItems.Add(connectedClient.Name + " is sending encrypted Data");
            }

        }
        private void UpdateListView(string ip, ConnectedClient connectedClient)
        {
            if (ui_lstvw_connectedClients.Items.Count > 0)
            {
                foreach (ListViewItem LVI in ui_lstvw_connectedClients.Items)
                {
                    if (LVI.Text == ip)
                    {
                        ui_lstvw_connectedClients.Items.Remove(LVI);
                    }
                }
            }
            else
                Text = "No One is Connected";
        }
        private void BroadcastMessage(MessageFrame mf, ConnectedClient conClient)
        {
            foreach (ConnectedClient clientsConnect in clients)
            {
                clientsConnect.ReSerializeData(mf, conClient);
            }

        }
        private void ClientInformation(ConnectedClient joiningClient)
        {
            
                foreach (ConnectedClient cc in clients)
                {
                    if (cc == joiningClient)
                        continue;
                    if (cc.Name == joiningClient.Name)
                    {
                        joiningClient.ClientsNameAlreadyInUse(joiningClient);
                        clients.Remove(joiningClient);
                        try
                        {
                            if (InvokeRequired)
                            {
                                Invoke(new delVoidStringConnectedClient(UpdateListView), joiningClient.ipAddress, joiningClient);
                                return;
                            }
                        }
                        catch(ObjectDisposedException err)
                        {
                            Console.WriteLine(err.Message + " : " + err.Source);
                        }
                    }
                }
            
            //tell currently connected clients about the new client
            foreach (ConnectedClient clientsOnline in clients)
            {
                clientsOnline.SendOutClientStatus(joiningClient);

            }
            try
            {
                Invoke(new delVoidStringConnectedClient(InitalizeListView), joiningClient.ipAddress, joiningClient);
            }
            catch(ObjectDisposedException err)
            {
                Console.WriteLine(err.Message + " : " + err.Source);
            }
            //tell the new client about the currently connected clients
            foreach (ConnectedClient clientAlreadyOnline in clients)
            {
                ClientInfoFrame ClientInfoFrameUpdater = (new ClientInfoFrame { Name = clientAlreadyOnline.Name, Joining = true, UsingEncryption = clientAlreadyOnline.encryptedOn });

                if (ClientInfoFrameUpdater.Name == joiningClient.Name)
                {
                    continue;
                }
                joiningClient.SendOutClientStatus(clientAlreadyOnline);

            }
        }

        private void ClientHasDisconnected(ConnectedClient disconnectedClient)
        {
              if (disconnectedClient.nameTakenDisconnect == false)
              {
                  lock (clients)
                  {
                      foreach (ConnectedClient clientsStillOnline in clients)
                      {
                          ClientInfoFrame ClientInfoFrameNotifier = (new ClientInfoFrame { Name = disconnectedClient.Name, Joining = false, UsingEncryption = disconnectedClient.encryptedOn });
                          clientsStillOnline.SendOutClientDisconnect(disconnectedClient);
                      }
                  }
                    //removes the disconnected client from the list
                    clients.Remove(disconnectedClient);
                    //updates the list view to show who is still online
                    
                    try
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new delVoidStringConnectedClient(UpdateListView), disconnectedClient.ipAddress, disconnectedClient);
                        }
                    }
                  catch(ObjectDisposedException err)
                    {
                        Console.WriteLine(err.Message+" : "+err.Source);
                    }
                }
            
            
        }
        


        private void HandleError(string err)
        {
            MessageBox.Show(err, "Socket Error!");
        }

        private void Server_Leave(object sender, EventArgs e)
        {

        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {

            foreach (ConnectedClient cc in clients)
            {
                cc.ServerDisconnect();
            }
        }
    }
}
