using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.Data
{
    [XmlType("Component")]
    public class ComponentDefinition
    {
        public Id Id;
        public string DisplayName;
        public string Icon;
    }
}
