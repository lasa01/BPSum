using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.World
{
    [XmlType("ModItem")]
    public class Mod
    {
        [XmlAttribute]
        public string FriendlyName;
        public string Name;
        [XmlElement("PublishedFileId")]
        public string Directory;
    }
}
