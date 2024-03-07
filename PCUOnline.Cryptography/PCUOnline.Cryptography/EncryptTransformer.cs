using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PCUOnline.Cryptography
{
    public class EncryptTransformer
    {
        // Methods
        public EncryptTransformer(EncryptionAlgorithm algId)
        {
            this.algorithmID = algId;
        }
        public ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey, byte[] initVec)
        {
            DES des1;
            TripleDES edes1;
            RC2 rc1;
            Rijndael rijndael1;
            switch (this.algorithmID)
            {
                case EncryptionAlgorithm.Des:
                    des1 = new DESCryptoServiceProvider();
                    des1.Mode = CipherMode.CBC;
                    if (null != bytesKey)
                    {
                        des1.Key = bytesKey;
                        this.encKey = des1.Key;
                        break;
                    }
                    this.encKey = des1.Key;
                    break;

                case EncryptionAlgorithm.Rc2:
                    rc1 = new RC2CryptoServiceProvider();
                    rc1.Mode = CipherMode.CBC;
                    if (null != bytesKey)
                    {
                        rc1.Key = bytesKey;
                        this.encKey = rc1.Key;
                        goto Label_014D;
                    }
                    this.encKey = rc1.Key;
                    goto Label_014D;

                case EncryptionAlgorithm.Rijndael:
                    rijndael1 = new RijndaelManaged();
                    rijndael1.Mode = CipherMode.CBC;
                    if (null != bytesKey)
                    {
                        rijndael1.Key = bytesKey;
                        this.encKey = rijndael1.Key;
                        goto Label_01BF;
                    }
                    this.encKey = rijndael1.Key;
                    goto Label_01BF;

                case EncryptionAlgorithm.TripleDes:
                    edes1 = new TripleDESCryptoServiceProvider();
                    edes1.Mode = CipherMode.CBC;
                    if (null != bytesKey)
                    {
                        edes1.Key = bytesKey;
                        this.encKey = edes1.Key;
                        goto Label_00DB;
                    }
                    this.encKey = edes1.Key;
                    goto Label_00DB;

                default:
                    throw new CryptographicException("Algorithm ID '" + this.algorithmID + "' not supported.");
            }
            if (null == initVec)
            {
                initVec = des1.IV;
            }
            else
            {
                des1.IV = initVec;
            }
            return des1.CreateEncryptor();
        Label_00DB:
            if (null == initVec)
            {
                initVec = edes1.IV;
            }
            else
            {
                edes1.IV = initVec;
            }
            return edes1.CreateEncryptor();
        Label_014D:
            if (null == initVec)
            {
                initVec = rc1.IV;
            }
            else
            {
                rc1.IV = initVec;
            }
            return rc1.CreateEncryptor();
        Label_01BF:
            if (null == initVec)
            {
                initVec = rijndael1.IV;
            }
            else
            {
                rijndael1.IV = initVec;
            }
            return rijndael1.CreateEncryptor();

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
        private EncryptionAlgorithm algorithmID;
        private byte[] encKey;
        private byte[] initVec;

    }
}
