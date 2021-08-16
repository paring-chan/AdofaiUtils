// using AdofaiUtils2.UI;
// using UnityEngine;

using AdofaiUtils2.UI;

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
            UIFactory.Init();
            SettingsUI.Init();
            Utils.Utils.RenderTweakSettings();
        }
    }
}