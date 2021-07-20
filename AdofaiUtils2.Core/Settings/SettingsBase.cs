using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public abstract class SettingsBase
    {
        private static readonly string SettingsPath = Path.Combine("Options", "AdofaiUtils2");

        [XmlIgnore]
        public string FilePath => Path.Combine(SettingsPath, $"{Id}.xml");

        public void SaveSettings<T>(T data)
        {
            Directory.CreateDirectory(SettingsPath);
            using (var writer = new StreamWriter(FilePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, data);
            }
        }
        
        [XmlIgnore]
        public string TabName;

        [XmlIgnore]
        public string Id;

        public static void Load()
        {
        }

        public abstract void GUI();
        
        public abstract void Save();
    }
}