using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library.Blueprint
{
    [XmlType("Definitions")]
    public class BlueprintDefinitions
    {
        public ShipBlueprint[] ShipBlueprints;
    }
}
