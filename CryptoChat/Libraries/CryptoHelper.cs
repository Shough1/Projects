// /////////////////////////////////////////////////////////////////////////////
// CryptoChat CryptoHelper
// CryptoHelper.cs
//
// This class contains a series of static methods that assist with performing
// cryptographic tasks for both the client and the server.
//
// 2015.03.01
// Joey Goertzen
// Shawn Hough
// CMPE2800
// /////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public class CryptoHelper
    {
        static private BinaryFormatter _bf = new BinaryFormatter();

        //generic method that encrypts any serializable type using a provided DES object
        static public byte[] Encrypt<T>(T frame, DESCryptoServiceProvider des)
        {
            //first convert the frame to a memory stream in preparation for encryption
            var msFrame = new MemoryStream();
            _bf.Serialize(msFrame, frame);
            
            //use a CryptoStream object and tell it to where to write the encrypted data
            var msFrameEncrypted = new MemoryStream();
            var cs = new CryptoStream(
                msFrameEncrypted, // <------------ write the encrypted data to this
                des.CreateEncryptor(), 
                CryptoStreamMode.Write);
            
            //write encrypted data to msFrameEncrypted (encryption takes place here)
            cs.Write(msFrame.GetBuffer(), 0, (int)msFrame.Length);
            cs.FlushFinalBlock();

            //convert encrypted memory stream to encrypted byte array 
            //in preparation for packaging into a CryptoFrame
            var length = (int)msFrameEncrypted.Length;
            var encryptedData = new byte[length];
            Array.Copy(msFrameEncrypted.GetBuffer(), encryptedData, length);

            return encryptedData;
        }

        //decrypts data previously encrypted with the specified DES object that is "loaded"
        //with the appropriate key and initialization vector (this should be the case assuming
        //a successful handshake occurred between the client and the server).
        //returns a deserialized object of the decrypted data.
        static public object DecryptToObject(byte[] dataEncrypted, DESCryptoServiceProvider des)
        {
            var msPayloadEncrypted = new MemoryStream(dataEncrypted);
            var msPayloadDecrypted = new MemoryStream();

            var cs = new CryptoStream(
                msPayloadEncrypted, 
                des.CreateDecryptor(), 
                CryptoStreamMode.Read);
                        
            cs.CopyTo(msPayloadDecrypted);  //actual decryption takes place here

            msPayloadDecrypted.Seek(0, SeekOrigin.Begin);
            var data = _bf.Deserialize(msPayloadDecrypted);
            
            return data;
        }

        //contructs a KeyFrame that contains the client's secret key information encrypted with
        //RSA using the specified public key string (should have been provided to the client 
        //by the server during the handshake).
        //should only be used by the client to transfer the secret key to the server
        //ie. Client uses; Server no touch!!!!!
        static public KeyFrame BuildKeyFrame(string publicKey, DESCryptoServiceProvider des)
        {
            //generate each part of the secret key (pssst.... this is where the secret key is born!)
            des.GenerateKey();
            des.GenerateIV();

            //load the RSA object with the server's public key
            var rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(publicKey);
            
            //construct KeyFrame with encrypted secret key data
            var keyFrame = new KeyFrame
            {
                Key = rsa.Encrypt(des.Key, false),
                IV = rsa.Encrypt(des.IV, false),
            };

            return keyFrame;
        }

        //debugging method used to verify the client and server are using the same secret key
        //(hint: they previously were not)
        static public void PrintDES(DESCryptoServiceProvider des, string source)
        {
            if(des == null)
            {
                Console.WriteLine("DES object is null; logging cancelled.");
                return;
            }

            des.IV.ToList().ForEach(e => Console.WriteLine(string.Format( "[{1}]\t iv: {0}", e, source)));
            des.Key.ToList().ForEach(e => Console.WriteLine(string.Format("[{1}]\tkey: {0}", e, source)));
        }
    }
}
