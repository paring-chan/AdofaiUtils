using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public static class SettingsManager
    {
        public static readonly Dictionary<string, SettingsBase> SettingsMap = new Dictionary<string, SettingsBase>();

        public static T Load<T>() where T : SettingsBase, new()
        {
            var t = new T();
            var path = t.FilePath;
            if (File.Exists(path))
            {
                try
                {
                    using (var stream = File.OpenRead(path))
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        var result = (T)serializer.Deserialize(stream);
                        return result;
                    }
                }
                catch (Exception e)
                {
                    CoreModule.ModEntry.Logger.Error($"Loading settings from {path} failed.");
                    CoreModule.ModEntry.Logger.LogException(e);
                }
            }

            return t;
        }
        
        public static void Register(SettingsBase settings)
        {
            SettingsBase s;
            if (!SettingsMap.TryGetValue(settings.Id, out s))
            {
                SettingsMap[settings.Id] = settings;
                Debug.Log($"Registered setting with id {settings.Id}");
            }
            else
            {
                Debug.Log($"Settings with id {s.Id} is already registered");
            }
        }

        public static void Unregister(SettingsBase settings)
        {
            if (SettingsMap[settings.Id] == null)
            {
                Debug.Log($"Settings with id {settings.Id} is already registered");
            }
            else
            {
                SettingsMap.Remove(settings.Id);
                Debug.Log($"UnRegistered setting with id {settings.Id}");
            }
        }
    }
}