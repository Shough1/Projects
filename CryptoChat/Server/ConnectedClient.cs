// /////////////////////////////////////////////////////////////////////////////
// CryptoChat Connected Client
// ConnectedClient.cs
// Helps moderate and extract data sent by each client.   
// Backend processor for deserializing and serializing information sent from a client.
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoLibrary;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Net;

namespace Server
{
    class ConnectedClient
    {
        public string Name;                                                                                                                     
        public delegate void delVoidConnectedClient(ConnectedClient clients);
        public delegate void delVoidMessageFrameConnectedClient(MessageFrame mf, ConnectedClient conClient);
        public delVoidMessageFrameConnectedClient receivedMessage = null;
        //public delegate void delVoidConnectedClient(ConnectedClient conClient);
        public delVoidConnectedClient clientsOnlineInfo = null;
        public delVoidConnectedClient clientHasDisconnected = null;
        public delVoidConnectedClient clientInformation = null;
        //should add another delegate here: clientHasDisconnected
        private ConnectionHelper connection;
        private ClientInfoFrame CIF;
        private RSACryptoServiceProvider _rsa;
        private DESCryptoServiceProvider _des;
        private KeyFrame KF;
        public bool IsDisconnected = false;
        private object DecryptedData;
        volatile public bool encryptedOn;
        volatile public bool nameTakenDisconnect;
        public string ipAddress;


        public ConnectedClient(ConnectionHelper connClient, RSACryptoServiceProvider r)
        {
            connection = connClient;
            connection.OnDisconnect = Disconnected;
            _rsa = r;
            _des = new DESCryptoServiceProvider();
            ipAddress = connection.IpAddress;
            InvokeFrames();
            nameTakenDisconnect = false;
        }

        public void Disconnected(string s)
        {
            connection.Disconnect();
            IsDisconnected = true;
            ClientInfoFrame clientDisconnect = (new ClientInfoFrame { Joining = false, Name = this.Name, UsingEncryption = encryptedOn });
            clientHasDisconnected(this);
            //should call clientHasDisconnected here to notify server that the client has disconnected
        }

        public void ServerDisconnect()
        {
            connection.Disconnect();
        }
        private void DeserializeData(object o)
        {

            if (o is ClientInfoFrame)
            {

                try
                {
                    CIF = (ClientInfoFrame)o;
                    Name = CIF.Name;
                    CIF.UsingEncryption = false;
                    CIF.Joining = true;
                    clientsOnlineInfo(this);
                    encryptedOn = false;

                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message + " :ConnectedClient:Deserialization:PublicKey");

                }
            }
            else if (o is KeyFrame)
            {
                try
                {
                    KF = (KeyFrame)o;
                    PrivateKey(KF);
                    encryptedOn = true;
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message + " :ConnectedClient:Deserialization:KeyFrame");
                }
            }

            else if (o is MessageFrame)
            {
                try
                {
                    MessageFrame mf = (MessageFrame)o;
                    encryptedOn = false;
                    receivedMessage(mf, this);

                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message + " :ConnectedClient:Deserialization:MessageFrame");

                }
            }
            else if (o is RequestFrame)
            {
                string s = _rsa.ToXmlString(false);
                connection.Send<ResponseFrame>(new ResponseFrame { PublicKey = s });
                encryptedOn = true;
            }
            else if (o is CryptoFrame)
            {
                CryptoFrame CF = (CryptoFrame)o;
                DecryptCryptoFrame(CF);
                encryptedOn = true;

            }
            else
            {
                connection.Disconnect();
            }

        }

        private void PrivateKey(KeyFrame privateKey)
        {
            _des.IV = _rsa.Decrypt(privateKey.IV, false);
            _des.Key = _rsa.Decrypt(privateKey.Key, false);
        }


        public void ReSerializeData(MessageFrame clientMessage, ConnectedClient senderClient)
        {

            if (clientMessage is MessageFrame)
            {
                if (encryptedOn == false)
                {
                    MessageFrame mf = (MessageFrame)clientMessage;
                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, mf);
                    Byte[] serBuff = new Byte[(int)ms.Length];
                    Array.Copy(ms.GetBuffer(), serBuff, (int)ms.Length);
                    DateTime dt = System.DateTime.Now;
                    connection.Send<MessageFrame>(new MessageFrame { Message = mf.Message, Sender = senderClient.Name, Datetime = dt.ToShortTimeString().ToString() });
                }
                else
                {
                    MessageFrame mf = (MessageFrame)clientMessage;
                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream();
                    bf.Serialize(ms, mf);
                    Byte[] serBuff = new Byte[(int)ms.Length];
                    Array.Copy(ms.GetBuffer(), serBuff, (int)ms.Length);
                    DateTime dt = System.DateTime.Now;
                    MessageFrame ToEncode = (new MessageFrame { Message = mf.Message, Sender = senderClient.Name, Datetime = dt.ToShortTimeString().ToString() });
                    EncryptMessageFrameAndSendOut(ToEncode);
                }
            }
        }
        private void DecryptCryptoFrame(CryptoFrame crypticData)
        {
            //CryptoHelper.PrintDES(_des,"Server");
            DecryptedData = CryptoHelper.DecryptToObject(crypticData.Payload, _des);

            if (DecryptedData is MessageFrame)
            {
                MessageFrame SendOutMessage = (MessageFrame)DecryptedData;
                receivedMessage(SendOutMessage, this);
            }
            if (DecryptedData is ClientInfoFrame)
            {
                ClientInfoFrame SendOutClientInfo = (ClientInfoFrame)DecryptedData;
                Name = SendOutClientInfo.Name;
                SendOutClientInfo.UsingEncryption = true;
                SendOutClientInfo.Joining = true;
                clientsOnlineInfo(this);
            }
        }
        private void EncryptMessageFrameAndSendOut(MessageFrame ToEncode)
        {
            byte[] encodedMessage = CryptoHelper.Encrypt(ToEncode, _des);
            connection.Send<CryptoFrame>(new CryptoFrame { Payload = encodedMessage });
        }
        private void EncryptClientInfoFrameAndSendOut(ClientInfoFrame ToEncode)
        {
            byte[] encodedMessages = CryptoHelper.Encrypt(ToEncode, _des);
            connection.Send<CryptoFrame>(new CryptoFrame { Payload = encodedMessages });
        }

        public void InvokeFrames()
        {
            connection.DataHandler = DeserializeData;
        }


        //Refactoring Opportunity: the "ClientInfoFrame cf" is not needed here and can be safely removed (the "if" statement would need to go too)
        public void SendOutClientStatus(ConnectedClient clientsOnline)
        {
            if (encryptedOn == false)
            {
                connection.Send<ClientInfoFrame>(new ClientInfoFrame { Joining = true, Name = clientsOnline.Name, UsingEncryption = clientsOnline.encryptedOn });
            }
            else
            {
                ClientInfoFrame ClientInfoFrameToEncode = (new ClientInfoFrame { Joining = true, Name = clientsOnline.Name, UsingEncryption = clientsOnline.encryptedOn });
                EncryptClientInfoFrameAndSendOut(ClientInfoFrameToEncode);
            }

        }

        //Refactoring Opportunity: the "ClientInfoFrame cf" is not needed here and can be safely removed (the "if" statement would need to go too and
        //"cf.Name" could be replaced with "clientOnStill.Name")
        public void SendOutClientDisconnect(ConnectedClient clientThatDisco)
        {
            if (encryptedOn == false)
            {
                connection.Send<ClientInfoFrame>(new ClientInfoFrame { Joining = false, Name = clientThatDisco.Name });
            }
            else
            {
                ClientInfoFrame ClientInfoFrameToEncode = (new ClientInfoFrame { Joining = false, Name = clientThatDisco.Name });
                EncryptClientInfoFrameAndSendOut(ClientInfoFrameToEncode);
            }
        }
        public void ClientsNameAlreadyInUse(ConnectedClient clientToChangeName)
        {
            if (encryptedOn == false)
            {
                connection.Send<ClientInfoFrame>(new ClientInfoFrame { Name = clientToChangeName.Name, nameTaken = true });
                nameTakenDisconnect = true;
            }
            else
            {
                ClientInfoFrame ClientInfoFrameToEncode = (new ClientInfoFrame { Name = clientToChangeName.Name, nameTaken = true });
                EncryptClientInfoFrameAndSendOut(ClientInfoFrameToEncode);
                nameTakenDisconnect = true;
            }
        }
    }
}