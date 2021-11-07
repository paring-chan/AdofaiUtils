using System.IO;
using AdofaiUtils.Config;
using UnityEngine;

namespace AdofaiUtils.Utils
{
    internal static class ConfigLoader
    {
        public static ConfigObject Config;

        private static readonly string FilePath = Path.Combine("Options", "AdofaiUtils.json");

        public static void Load()
        {
            if (!File.Exists(FilePath))
            {
                Config = new ConfigObject();
                return;
            }
            
            Config = JsonUtility.FromJson<ConfigObject>(FilePath);
        }

        public static void Save()
        {
            File.WriteAllText(FilePath, JsonUtility.ToJson(Config, true));
        }
    }
}