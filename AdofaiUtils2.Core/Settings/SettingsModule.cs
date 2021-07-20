using HarmonyLib;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    internal class SettingsModule
    {
        public static SettingsModule Instance { get; private set; }

        public CoreSettings Settings;

        private GameObject GameObject;

        private CoreSettings _settings;

        public SettingsModule()
        {
            Instance = this;
        }

        public void Init()
        {
            GameObject = new GameObject();
            GameObject.AddComponent<SettingsUI>();
            Object.DontDestroyOnLoad(GameObject);
            Settings = SettingsManager.Load<CoreSettings>();
            SettingsManager.Register(Settings);
        }

        public void Destroy()
        {
            Object.Destroy(GameObject);
            GameObject = null;
            SettingsManager.Unregister(Settings);
        }
    }
}