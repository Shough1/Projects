// /////////////////////////////////////////////////////////////////////////////
// CryptoChat Connection Helper
// ConnectionHelper.cs
//
// This class handles general socket communication. In this project, is the foundation
// of communication between a client and the server. It contains delegates that
// can be used to inform the outside world of events that are occurring inside.
//
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace CryptoLibrary
{
    public class ConnectionHelper
    {
        //allow communication with the outside world
        public delegate void delVoidObj(object o);
        public delegate void delVoidString(string s);

        public delVoidObj DataHandler = null;       //called when an object has been received
        public delVoidString OnDisconnect = null;   //called when the socket is disconnected

        static private BinaryFormatter _bf = new BinaryFormatter();     //tool for serializing/deserializing objects
        
        public string IpAddress;                //provide public access to the IP address of whoever this
                                                //Connection Helper is connected to

        private Socket _connectedSocket;        //represents the connection between this Connection Helper and another socket

        private Queue<byte[]> _sendQueue;       //things to be sent are tossed in here
        private Queue<object> _receiveQueue;    //things that have been received and need to be processed are tossed in here

        private volatile bool _runThreads;      //set this to turn off all threads

        private Thread _sendThread;             //sends data to the connected socket
        private Thread _receiveThread;          //receives data from the connected socket
        private Thread _dequeueThread;          //decides how to process received data
        
        public ConnectionHelper(Socket s)
        {
            IpAddress = s.RemoteEndPoint.ToString();
            
            _connectedSocket = s;
            
            _sendQueue = new Queue<byte[]>();
            _receiveQueue = new Queue<object>();

            _runThreads = true;

            //let communication begin
            _sendThread = startThread(SendingThreadMethod);
            _dequeueThread = startThread(DequeInfo);
            _receiveThread = startThread(ReceivingThread);
        }

        //sends raw bytes to the connected socket
        public void Send(byte[] data)
        {
            lock (_sendQueue)
                _sendQueue.Enqueue(data);
        }

        //sends frames to the connected socket
        public void Send<T>(T frame)
        {
            Send(SerializeFrame<T>(frame));
        }

        //keeps the ui thread free of managing sending operations
        private void SendingThreadMethod()
        {
            byte[] data;
            bool empty;

            while (_runThreads)
            {
                Thread.Sleep(1);

                do
                {
                    //determine if there is anything to be sent
                    lock (_sendQueue)
                        empty = _sendQueue.Count == 0;

                    //break out of inner while loop if there is currently nothing to send
                    if (empty)
                        break;

                    lock (_sendQueue)
                        data = _sendQueue.Dequeue();

                    try
                    {
                        _connectedSocket.Send(data, (int)data.Length, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send buffer: " + ex.Message);
                    }

                }
                while (true);
            }
        }

        //keeps the ui thread free of managing receiving operations
        private void ReceivingThread()
        {
            var msReceive = new MemoryStream();     //pile received data onto this
            var buffer = new byte[10000];           //catch batches of received data with this
            int bytesReceived;                      //keep track of quantities received
            object receivedObject;                  //destack objects into this

            //this connection will terminate if nothing has been received for 10 whole minutes (600,000 ms)
            _connectedSocket.ReceiveTimeout = 600000;

            while (_runThreads)
            {
                try
                {
                    bytesReceived = _connectedSocket.Receive(buffer);
                }
                catch (SocketException) //hard disconnect
                {
                    //in case the form is disposed, this will allow a graceful shutdown
                    try
                    {
                        if (OnDisconnect != null)
                            OnDisconnect("Hard disconnect"); //notify outside world
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }

                    return;
                }

                if(bytesReceived == 0)
                {
                    //in case the form is disposed, this will allow a graceful shutdown
                    try
                    {
                        if (OnDisconnect != null)
                            OnDisconnect("Soft disconnect"); //notify outside world
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }

                    return;
                }

                UpdateReceiveStream(msReceive, buffer, bytesReceived);
                
                //destack objects until the receive stream is empty or a fragment occurs
                do
                {
                    long lStart = msReceive.Position;   //in case fragmentatio occurs
                    
                    try
                    {
                        receivedObject = _bf.Deserialize(msReceive);
                    }
                    catch(SerializationException) //fragment
                    {
                        msReceive.Position = lStart;
                        break;
                    }

                    //at this point, an object has been destacked
                    lock (_receiveQueue)
                        _receiveQueue.Enqueue(receivedObject);
                }
                while (msReceive.Position < msReceive.Length);

                //keep the size of the receive stream in check
                if (msReceive.Position == msReceive.Length)
                {
                    msReceive.Position = 0;
                    msReceive.SetLength(0);
                }
                else if (msReceive.Position > 50000)
                {
                    trimReceiveStream(msReceive);
                }
            }
        }

        //processes received objects
        private void DequeInfo()
        {
            object information;
            bool informationExists;
            
            while (_runThreads)
            {
                //check if there is anything to process
                lock (_receiveQueue)
                    informationExists = _receiveQueue.Count > 0;

                if (informationExists)
                {
                    lock (_receiveQueue)
                        information = _receiveQueue.Dequeue();

                    //in case the form is disposed, this will allow a graceful shutdown
                    try
                    {
                        //the connection helper does not process received objects
                        if (DataHandler != null)
                            DataHandler(information);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        return;
                    }
                }
                else
                {
                   Thread.Sleep(1);
                }
                
            }
        }

        //stops all the threads and disconnects the socket
        public void Disconnect()
        {
            _runThreads = false;

            try
            {
                _connectedSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (_connectedSocket != null)
                _connectedSocket.Close();
        }

        //helper method to start a thread as a background thread
        private Thread startThread(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();
            return thread;
        }

        //generic method that serializes frames
        static public byte[] SerializeFrame<T>(T frame)
        {
            var ms = new MemoryStream();

            _bf.Serialize(ms, frame);

            var msLength = (int)ms.Length;
            var bytes = new byte[msLength];

            Array.Copy(ms.GetBuffer(), bytes, msLength);

            return bytes;
        }

        //appends newly received data to the end of the receive stream
        static public void UpdateReceiveStream(MemoryStream ms, byte[] bufferReceive, int bytesReceived)
        {
            long longReceivePosition = ms.Position; //remember where the cursor was
            
            ms.Seek(0, SeekOrigin.End);
            ms.Write(bufferReceive, 0, bytesReceived);
            
            ms.Position = longReceivePosition;      //restore cursor position
        }

        //discards everything except the tail-end frame fragment
        private void trimReceiveStream(MemoryStream msRXStream)
        {
            var memoryStream = new MemoryStream();

            //write the tail end into a new stream
            while (msRXStream.Position < msRXStream.Length)
            {
                var b = (byte)msRXStream.ReadByte();
                memoryStream.WriteByte(b);
            }

            //replace old stream with the new stream and start at the beginning
            msRXStream = memoryStream;
            msRXStream.Position = 0;

        }
    }
}
