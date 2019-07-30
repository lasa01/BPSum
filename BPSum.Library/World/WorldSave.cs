using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.World
{
    [XmlRoot("MyObjectBuilder_Checkpoint")]
    public class WorldSave
    {
        public Mod[] Mods;
    }
}
