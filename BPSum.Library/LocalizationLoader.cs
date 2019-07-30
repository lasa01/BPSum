using BPSum.Library.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BPSum.Library
{
    class LocalizationLoader
    {
        XmlSerializer serializer;

        public LocalizationLoader()
        {
            serializer = new XmlSerializer(typeof(LocalizationRoot));
        }

        public LocalizationData[] LoadFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                LocalizationRoot root = (LocalizationRoot)serializer.Deserialize(stream);
                return root.Datas;
            }
        }
    }
}
