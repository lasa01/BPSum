using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.Data
{
    [XmlType("Definitions")]
    public class DataDefinitions
    {
        public ComponentDefinition[] Components;
        public CubeBlockDefinition[] CubeBlocks;

        public bool HasAny => (Components != null && Components.Length != 0) || (CubeBlocks != null && CubeBlocks.Length != 0);
    }

    public class Id
    {
        public string TypeId;
        public string SubtypeId;
    }
}
