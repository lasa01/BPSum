using BPSum.Library.Blueprint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library
{
    class BlueprintLoader
    {
        XmlSerializer serializer;

        public BlueprintLoader()
        {
            serializer = new XmlSerializer(typeof(BlueprintDefinitions));
        }

        public ShipBlueprint LoadFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BlueprintDefinitions def = (BlueprintDefinitions)serializer.Deserialize(XmlRemoveTypes.RemoveTypes(stream));
                return def.ShipBlueprints.First();
            }
        }
    }
}
