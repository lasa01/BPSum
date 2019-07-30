using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.Data
{
    [XmlType("Definition")]
    public class CubeBlockDefinition
    {
        public string Type;
        public Id Id;
        public string DisplayName;
        public string Icon;
        public bool Public;
        public CubeBlockComponent[] Components;
    }

    [XmlType("Component", Namespace = "CubeBlock")]
    public class CubeBlockComponent
    {
        [XmlAttribute]
        public string Subtype;
        [XmlAttribute]
        public uint Count;
        [XmlIgnore]
        public ComponentDefinition Component;
    }
}
