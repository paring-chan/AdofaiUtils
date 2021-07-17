using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class CoreSettings : SettingsBase
    {
        public CoreSettings()
        {
            TabName = "설정";
    
            Id = "AdofaiUtils2.Core.CoreSettings";
        }

        public override void GUI()
        {
            GUILayout.Label("와아아");
        }
    }
}