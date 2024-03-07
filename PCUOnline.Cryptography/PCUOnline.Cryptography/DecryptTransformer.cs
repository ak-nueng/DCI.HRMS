using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PCUOnline.Cryptography
{
    public class DecryptTransformer
    {
        // Methods
        public DecryptTransformer(EncryptionAlgorithm deCryptId)
        {
            this.algorithmID = deCryptId;
        }

        public ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey, byte[] initVec)
        {
            switch (this.algorithmID)
            {
                case EncryptionAlgorithm.Des:
                    {
                        DES des1 = new DESCryptoServiceProvider();
                        des1.Mode = CipherMode.CBC;
                        des1.Key = bytesKey;
                        des1.IV = initVec;
                        return des1.CreateDecryptor();
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc1 = new RC2CryptoServiceProvider();
                        rc1.Mode = CipherMode.CBC;
                        return rc1.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael1 = new RijndaelManaged();
                        rijndael1.Mode = CipherMode.CBC;
                        return rijndael1.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES edes1 = new TripleDESCryptoServiceProvider();
                        edes1.Mode = CipherMode.CBC;
                        return edes1.CreateDecryptor(bytesKey, initVec);
                    }
            }
            throw new CryptographicException("Algorithm ID '" + this.algorithmID + "' not supported.");
        }

        // Properties
        public byte[] IV { get { return initVec; } set { initVec = value; } }

        // Fields
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;

    }
}
