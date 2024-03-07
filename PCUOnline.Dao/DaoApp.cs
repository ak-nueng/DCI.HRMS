using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;

namespace PCUOnline.Dao
{
    public class DaoApp
    {
        public static string GetDatabase(string applicationId)
        {
            try
            {
                string text1 = string.Format("{0}[@id='{1}']", "/dataConfiguration/application", applicationId);

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(ConfigurationPath);

                XmlNodeList list1 = xdoc.DocumentElement.SelectNodes(text1);
                if ((list1 == null) || (list1.Count <= 0))
                    throw new ArgumentNullException("Application not found for " + applicationId + ".");

                return list1[0].Attributes["value"].Value;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string ConfigurationPath
        {
            get
            {
                Assembly assembly1 = Assembly.GetExecutingAssembly();
                string text1 = Path.GetDirectoryName(assembly1.CodeBase);
                return (text1 + @"\dbconfig.xml");
            }
        }
    }
}
