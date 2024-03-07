using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace PCUOnline.Cryptography
{
    public class Decryptor
    {
        // Methods
        public Decryptor(EncryptionAlgorithm algId)
        {
            this.transformer = new DecryptTransformer(algId);
        }

        public byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
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
            stream2.FlushFinalBlock();
            stream2.Close();
            return stream1.ToArray();
        }
        public string Decrypt(string data, string key)
        {
            string text1 = string.Empty;
            try
            {
                this.IV = Encoding.ASCII.GetBytes(key);
                byte[] buffer1 = Encoding.ASCII.GetBytes(key);
                byte[] buffer2 = Decrypt(Convert.FromBase64String(data), buffer1);
                text1 = Encoding.ASCII.GetString(buffer2);
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
            set
            {
                this.initVec = value;
            }
        }

        // Fields
        private byte[] initVec;
        private DecryptTransformer transformer;

    }
}
