using BPSum.Library.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library
{
    class WorldLoader
    {
        XmlSerializer serializer;
        public WorldLoader()
        {
            serializer = new XmlSerializer(typeof(WorldSave));
        }

        public Mod[] LoadFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                WorldSave save = (WorldSave) serializer.Deserialize(stream);
                return save.Mods;
            }
        }
    }
}
