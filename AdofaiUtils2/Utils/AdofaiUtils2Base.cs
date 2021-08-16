using AdofaiUtils2.Settings;
using AdofaiUtils2.UI;

namespace AdofaiUtils2.Utils
{
    public class AdofaiUtils2Base
    {
        public SettingsManager settings => SettingsManager.Instance;
        public UIManager ui => UIManager.Instance;
    }
}