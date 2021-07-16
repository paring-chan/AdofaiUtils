using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsModule
    {
        public static SettingsModule Instance
        {
            get;
            private set;
        }
        
        private GameObject GameObject;

        public SettingsModule()
        {
            Instance = this;
            GameObject = new GameObject();
            GameObject.AddComponent<Canvas>();
            GameObject.AddComponent<SettingsBehaviour>();
            Object.DontDestroyOnLoad(GameObject);
        }
    }
}