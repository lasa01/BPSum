using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BPSum.Library
{
    static class XmlRemoveTypes
    {
        public static XmlReader RemoveTypes(FileStream stream)
        {
            // Get rid of all xsi:type attributes since they specify unneeded subclasses for blocks and break deserialization
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            XmlNodeList xnList = doc.SelectNodes("//*[@xsi:type]", nsmgr);
            foreach (XmlNode xn in xnList)
            {
                xn.Attributes.RemoveNamedItem("xsi:type");
            }
            return XmlReader.Create(new StringReader(doc.OuterXml));
        }
    }
}
