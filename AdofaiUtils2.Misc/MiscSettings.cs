using AdofaiUtils2.Core.Settings;
using UnityEngine;

namespace AdofaiUtils2.Misc
{
    public class MiscSettings : SettingsBase
    {
        public MiscSettings()
        {
            Id = "AdofaiUtils2.Misc.MiscSettings";
            TabName = "기타";
        }

        public override void GUI()
        {
            GUILayout.Label("기타 설정");
        }

        public override void Save()
        {
            SaveSettings(this);
        }
    }
}