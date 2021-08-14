using AdofaiUtils2.UI;

namespace AdofaiUtils2.Settings
{
    public class SettingsManager
    {
        public static SettingsManager Instance;
        public SettingsUI UI => SettingsUI.Instance;
        
        public static void Init()
        {
            Instance = new SettingsManager();
            SettingsUI.Init();
        }
    }
}