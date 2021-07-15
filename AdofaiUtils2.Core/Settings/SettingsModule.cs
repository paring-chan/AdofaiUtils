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
        
        public GameObject GameObject;

        public SettingsModule()
        {
            Instance = this;
            GameObject = Object.Instantiate(Assets.SettingsUI);
            Object.DontDestroyOnLoad(GameObject);
        }
    }
}