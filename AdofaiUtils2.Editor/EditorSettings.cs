using System.Reflection;
using AdofaiUtils2.Core.Settings;
using AdofaiUtils2.Core.Util;
using UnityEngine;

namespace AdofaiUtils2.Editor
{
    public class EditorSettings : SettingsBase
    {
        public EditorSettings()
        {
            TabName = "에디터";
            Id = "AdofaiUtils2.Editor";
        }
        
        public override void Save()
        {
            SaveSettings(this);
        }

        public bool ShowBeats;

        public override void OnGUI()
        {
            var showBeats = GUILayout.Toggle(ShowBeats, "선택한 타일의 비트수 표시");

            if (showBeats != ShowBeats)
            {
                ShowBeats = showBeats;
                if (showBeats)
                {
                    EditorModule.Harmony.PatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Editor.ShowBeats");
                }
                else
                {
                    EditorModule.Harmony.UnpatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Editor.ShowBeats");
                }
            }
        }
    }
}