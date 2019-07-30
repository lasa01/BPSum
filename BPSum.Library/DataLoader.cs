using BPSum.Library.Data;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BPSum.Library
{
    class DataLoader
    {
        XmlSerializer serializer;
        readonly Action<DataDefinitions> onData;

        public DataLoader(Action<DataDefinitions> onData)
        {
            serializer = new XmlSerializer(typeof(DataDefinitions));
            this.onData = onData;
        }

        public void LoadFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                DataDefinitions def = (DataDefinitions)serializer.Deserialize(XmlRemoveTypes.RemoveTypes(stream));
                if (def.HasAny)
                {
                    onData(def);
                }
            }
        }

        public void LoadDir(string path)
        {
            string[] files = Directory.GetFiles(path, "*.sbc", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                LoadFile(file);
            }
        }
    }
}
