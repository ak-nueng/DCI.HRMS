using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using PCUOnline.Cryptography;

namespace PCUOnline.Dao
{
    public class DaoProperty
    {
        private string facCls;
        private string facAsm;
        private string daoCls;
        private string daoAsm;
        private string connectionString;
        private int connectionTimeOut;

        public DaoProperty(string resource)
        {
            Init(resource);
        }

        private void Init(string resource)
        {
            try
            {
                SetDaoClass(resource);
                SetConnectionString(resource);
            }
            catch
            {
                throw;
            }
        }

        private void SetDaoClass(string resource)
        {
            XmlNodeList list1 = LoadApplication(resource);

            if ((list1 == null) || (list1.Count <= 0))
                throw new ArgumentNullException("Configuration Error: Resource not found for " + resource + ".");

            string CLASS_DAO_MANAGER = list1[0].Attributes["daoManager"].Value;
            string CLASS_DAO_FACTORY = list1[0].Attributes["daoFactory"].Value;

            daoAsm = GetAssembly(CLASS_DAO_MANAGER);
            daoCls = GetClass(CLASS_DAO_MANAGER);
            facAsm = GetAssembly(CLASS_DAO_FACTORY);
            facCls = GetClass(CLASS_DAO_FACTORY);
        }
        private void SetConnectionString(string resource)
        {
            string connectionName = LoadApplication(resource)[0].Attributes["connectionString"].Value;
            string text2 = string.Format("{0}[@name='{1}']"
                                        , "/dataConfiguration/connectionString/instance"
                                        , connectionName);

            XmlDocument document1 = new XmlDocument();
            document1.Load(DaoApp.ConfigurationPath);

            XmlNodeList list1 = document1.DocumentElement.SelectNodes(text2);

            if ((list1 == null) || (list1.Count <= 0))
                throw new ArgumentNullException("Configuration Error: ConnectionName not found for " + connectionName + ".");

            string DB_CONN_STRING = list1[0].Attributes["value"].Value;
            string DB_PUBLIC_KEY = list1[0].Attributes["publicKey"].Value;

            connectionString = Decrypt(DB_CONN_STRING, DB_PUBLIC_KEY);
            connectionTimeOut = 60;
        }
        
        private XmlNodeList LoadApplication(string resource)
        {
            XmlDocument document1 = new XmlDocument();
            document1.Load(DaoApp.ConfigurationPath);

            string text1 = string.Format("{0}[@id='{1}']", "/dataConfiguration/application", resource);
            XmlNodeList list1 = document1.DocumentElement.SelectNodes(text1);
            return list1;
        }

        private void Init_OD(string resource)
        {
            //string text1 = string.Format("{0}[@name='{1}']", "/dataConfiguration/instance", instanceName);

            try
            {
                XmlDocument document1 = new XmlDocument();
                document1.Load(DaoApp.ConfigurationPath);

                XmlNodeList list1 = document1.DocumentElement.SelectNodes(resource);
                if ((list1 == null) || (list1.Count <= 0))
                {
                    throw new ArgumentNullException("Invalid resource: " + resource + ".");
                }

                XmlNode node = list1[0];
                string t1 = node.Attributes["connectionString"].Value;
                string t2 = node.Attributes["publicKey"].Value;
                string t3 = node.Attributes["daoManager"].Value;
                string t4 = node.Attributes["daoFactory"].Value;

                daoAsm = GetAssembly(t3);
                daoCls = GetClass(t3);
                facAsm = GetAssembly(t4);
                facCls = GetClass(t4);

                connectionString = Decrypt(t1, t2);

                try
                {
                    connectionTimeOut = Convert.ToInt32(node.Attributes["timeOut"].Value);
                }
                catch { connectionTimeOut = 30; }
            }
            catch
            {
                throw;
            }
        }

        private string Decrypt(string data, string tokenKey)
        {
            string text1 = string.Empty;
            try
            {
                Decryptor decryptor1 = new Decryptor(EncryptionAlgorithm.Rijndael);
                text1 = decryptor1.Decrypt(data, tokenKey);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return text1;
        }
        private string Encrypt(string data, string tokenKey)
        {
            string text1 = string.Empty;
            try
            {
                Encryptor encryptor1 = new Encryptor(EncryptionAlgorithm.Rijndael);
                text1 = encryptor1.Encrypt(data, tokenKey);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return text1;
        }

        public string DaoManagerAssembly
        {
            get {
                return daoAsm;
            }
        }
        public string DaoManagerClass
        {
            get { return daoCls; }
        }
        public string DaoFactoryAssembly
        {
            get
            {
                return facAsm;
            }
        }
        public string DaoFactoryClass
        {
            get { return facCls; }
        }
        public string ConnectionString
        {
            get { return connectionString; }
        }
        public int ConnectionTimeOut
        {
            get { return connectionTimeOut; }
        }

        private string GetAssembly(string arg)
        {
            int pos = arg.IndexOf(',');
            return arg.Substring(0, pos - 1).Trim();
        }
        private string GetClass(string arg)
        {
            int pos = arg.IndexOf(',');
            return arg.Substring(pos+1).Trim();
        }

        # region Static Method
        
        public static string ConfigurationPath
        {
            get
            {
                Assembly assembly1 = Assembly.GetExecutingAssembly();
                string text1 = Path.GetDirectoryName(assembly1.CodeBase);
                return (text1 + @"\dbconfig.xml");
            }
        }

        # endregion
    }
}
