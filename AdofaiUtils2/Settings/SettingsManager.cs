// using AdofaiUtils2.UI;
// using UnityEngine;

namespace AdofaiUtils2.Settings
{
    public class SettingsManager
    {
        public static SettingsManager Instance;
        // public static GameObject TweakPrefab;
        public SettingsUI UI => SettingsUI.Instance;
        
        public static void Init()
        {
            Instance = new SettingsManager();
            // TweakPrefab = Assets.Bundle.LoadAsset<GameObject>("Assets/Prefab/Tweak.prefab");
            SettingsUI.Init();
        }
    }
}