using HarmonyLib;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsModule
    {
        public static SettingsModule Instance { get; private set; }

        private GameObject GameObject;

        public SettingsModule()
        {
            Instance = this;
        }

        public void Init()
        {
            GameObject = new GameObject();
            GameObject.AddComponent<SettingsUI>();
            Object.DontDestroyOnLoad(GameObject);
        }

        public void Destroy()
        {
            Object.Destroy(GameObject);
            GameObject = null;
        }
    }
}