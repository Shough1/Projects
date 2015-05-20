// /////////////////////////////////////////////////////////////////////////////
// CryptoChat Frames
// Frames.cs
//
// This file contains all of the frames that are used in communication between
// the client and the server. 
//
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////

using System;

namespace CryptoLibrary
{
    //first stage of 3-way handshake; no members
    [Serializable]
    public class RequestFrame { }

    //second stage of 3-way handshake; unencrypted public key
    [Serializable]
    public class ResponseFrame
    {
        public string PublicKey;    //the public portion of the server's asymmetric pair of keys
    }

    //third stage of 3-way handshake
    [Serializable]
    public class KeyFrame
    {
        public byte[] Key;      //encrypted private key
        public byte[] IV;       //encrypted initialization vector
    }

    //sent to the server initially before any messages are sent
    [Serializable]
    public class ClientInfoFrame
    {

        //ALSO: this frame is being reused by the server to inform clients of other connected clients;
        //we might consider using a separate frame as when the client initially sends this to the server,
        //Joining and UsingEncryption are not used (inefficient, but so far
        //it has not significantly impacted performance)

        public string Name;             //client provided user name
        public bool Joining;            //true if client has connected; false if disconnected
        public bool UsingEncryption;    //true if client is using encryption
        public bool nameTaken;          //true if a connected client is already on the server
    }

    //wraps encrypted data (ie. serialized MessageFrames and/or ClientInfoFrame)
    [Serializable]
    public class CryptoFrame
    {
        public byte[] Payload;      //encrypted data
    }

    //regular messages sent back-and-forth between the server and client
    [Serializable]
    public class MessageFrame
    {
        public string Message;      //the message to communicate
        public string Sender;       //who sent the message (server populated)
        public string Datetime;     //when did the server receive the message (server populated)
    }


}
