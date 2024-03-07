using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DCIBizPro.Util.Xml
{
    public class XmlGenerator
    {
        private XmlPreparation x_prepare = null;
        private StreamWriter sw = null;
        private string toFile = string.Empty;

        private XmlGenerator()
        {
        }
        public XmlGenerator(XmlPreparation preparation , string toFile)
        {
            this.x_prepare = preparation;
            this.toFile = toFile;
        }

        public XmlPreparation Preparation
        {
            get { return this.x_prepare; }
        }
        public string ToFie
        {
            get { return toFile; }
        }

        public void Start()
        {
            if(sw == null)
                sw = new StreamWriter(toFile, false, Encoding.UTF8);

            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sw.WriteLine(string.Format("<{0} xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">"
                                        , Preparation.DefineName));
        }
        public void Write(string xmlData)
        {
            if(xmlData != string.Empty)
                sw.WriteLine(xmlData);
        }
        public void Close()
        {
            if (sw != null)
            {
                sw.WriteLine(string.Format("</{0}>",Preparation.DefineName));
                sw.Close();
            }
        }
        
    }
}
