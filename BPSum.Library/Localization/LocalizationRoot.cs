using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.Localization
{
    [XmlRoot("root")]
    public class LocalizationRoot
    {
        [XmlElement("data")]
        public LocalizationData[] Datas;
    }
}
