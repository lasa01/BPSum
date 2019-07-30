using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.Localization
{
    [XmlType("data")]
    public class LocalizationData
    {
        [XmlAttribute("name")]
        public string Name;
        [XmlElement("value")]
        public string Value;
    }
}
