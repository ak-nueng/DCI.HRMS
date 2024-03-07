using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
using System.Diagnostics;

namespace DCIBizPro.Util.Xml
{
    public class XmlPreparation
    {
        private string xsd_File = string.Empty;
        private string xml_TempFile = string.Empty;
        private string defineName = string.Empty;

        private XmlDocument x_doc = null;
        private XmlNode x_node = null;

        public XmlPreparation(string xsdFile , string defineName)
        {
            this.xsd_File = xsdFile;
            this.defineName = defineName;
            PrepareXmlTemplate();
        }
        public string XsdFile
        {
            get { return xsd_File; }
            //set { xsd_File = value; }
        }
        public string DefineName
        {
            get { return defineName; }
        }
        public string TemporaryFile
        {
            get { return xml_TempFile; }
        }
        private void PrepareXmlTemplate()
        {
            xml_TempFile = xsd_File.Replace(".xsd","_Temp.xml");
            
            XmlSchemaSet schemas = new XmlSchemaSet();
            XmlQualifiedName qname = XmlQualifiedName.Empty;

            schemas.Add(null, xsd_File);

            XmlTemplateGenerator genr = new XmlTemplateGenerator(schemas, qname);
            XmlTextWriter textWriter = new XmlTextWriter(xml_TempFile, null);

            textWriter.Formatting = Formatting.Indented;
            genr.WriteXml(textWriter);

            LoadXmlDocument();
        }
        private void LoadXmlDocument()
        {
            x_doc = new XmlDocument();
            x_doc.Load(xml_TempFile);

            x_node = x_doc.DocumentElement.SelectSingleNode("/" + defineName);
            if (x_node == null)
                throw new Exception("Unknow xpath.");
        }
        public void New()
        {
            this.LoadXmlDocument();
        }
        public string Read()
        {
            if (x_node != null)
            {
                return x_node.InnerXml;
            }

            return string.Empty;
        }
        public void Set(string element, string value)
        {
            XmlNode node = x_node.SelectSingleNode(element);
            node.InnerText = value;
        }
        public void Set(string element, string attribute, string value)
        {
            XmlNode node = x_node.SelectSingleNode(element);
            node.Attributes[attribute].InnerText = value;
        }
        
        public void ViewData()
        {
            Debug.WriteLine("================== View Data ======================");
            Debug.WriteLine(x_node.InnerXml);
        }
    }
}
