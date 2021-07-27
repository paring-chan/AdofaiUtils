using AdofaiUtils2.Core.Settings;
using UnityEngine;

namespace AdofaiUtils2.Play
{
    public class PlaySettings : SettingsBase
    {
        public PlaySettings()
        {
            Id = "AdofaiUtils2.Play";
            TabName = "플레이";
        }
        
        public override void Save()
        {
            SaveSettings(this);
        }

        public bool Hide10;

        public override void OnGUI()
        {
            Hide10 = GUILayout.Toggle(Hide10, "1.0배 텍스트 숨기기");
        }
    }
}