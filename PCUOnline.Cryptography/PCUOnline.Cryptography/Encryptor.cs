using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace PCUOnline.Cryptography
{
    public class Encryptor
    {
        // Methods
        public Encryptor(EncryptionAlgorithm algId)
        {
            this.transformer = new EncryptTransformer(algId);
        }
        public byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
        {
            MemoryStream stream1 = new MemoryStream();
            ICryptoTransform transform1 = this.transformer.GetCryptoServiceProvider(bytesKey, this.initVec);
            CryptoStream stream2 = new CryptoStream(stream1, transform1, CryptoStreamMode.Write);
            try
            {
                stream2.Write(bytesData, 0, bytesData.Length);
            }
            catch (Exception exception1)
            {
                throw new Exception("Error while writing encrypted data to the stream: \n" + exception1.Message);
            }
            this.encKey = this.transformer.Key;
            stream2.FlushFinalBlock();
            stream2.Close();
            return stream1.ToArray();
        }
        public string Encrypt(string data, string key)
        {
            string text1 = string.Empty;
            try
            {
                byte[] buffer1 = Encoding.ASCII.GetBytes(data);
                byte[] buffer2 = Encoding.ASCII.GetBytes(key);
                this.IV = buffer2;
                byte[] buffer3 = Encrypt(buffer1, buffer2);
                text1 = Convert.ToBase64String(buffer3);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return text1;
        }
        // Properties
        public byte[] IV
        {
            get
            {
                return this.initVec;
            }
            set
            {
                this.initVec = value;
            }
        }
        public byte[] Key
        {
            get
            {
                return this.encKey;
            }
        }

        // Fields
        private byte[] encKey;
        private byte[] initVec;
        private EncryptTransformer transformer;


    }
}
