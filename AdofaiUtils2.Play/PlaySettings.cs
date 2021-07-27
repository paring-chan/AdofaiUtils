using System.Reflection;
using AdofaiUtils2.Core.Settings;
using AdofaiUtils2.Core.Util;
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
            var Hide10New = GUILayout.Toggle(Hide10, "1.0배 텍스트 숨기기");
            if (Hide10New != Hide10)
            {
                if (Hide10New)
                {
                    PlayModule.Harmony.PatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Play.Hide10");
                }
                else
                {
                    PlayModule.Harmony.UnpatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Play.Hide10");
                }

                Hide10 = Hide10New;
            }
        }
    }
}