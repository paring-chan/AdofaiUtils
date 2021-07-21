using System;
using System.Reflection;
using System.Xml.Serialization;
using AdofaiUtils2.Core.Settings;
using AdofaiUtils2.Core.Util;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Misc
{
    [Serializable]
    public class MiscSettings : SettingsBase
    {
        public MiscSettings()
        {
            Id = "AdofaiUtils2.Misc.MiscSettings";
            TabName = "기타";
        }

        [XmlIgnore]
        public bool keyBindCollapse;
        public bool keyBindEnabled;

        public bool instantJoinKeyActive;
        public KeyBinding instantJoinKey = new KeyBinding
        {
            keyCode = KeyCode.LeftArrow
        };
        [XmlIgnore]
        public bool instantJoinKeyCollapse;

        private void KeyMapUI(ref KeyBinding key, ref bool active, ref bool collapse, string title)
        {
            GUILayout.BeginHorizontal();
            var newActive = GUILayout.Toggle(collapse, active ? collapse ? "▼" : "▶" : "", new GUIStyle
            {
                fixedWidth = 10,
                margin = new RectOffset(0, 4, 0, 0),
                fontSize = 15,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            });
            var newChecked = GUILayout.Toggle(active, title, new GUIStyle(GUI.skin.toggle)
            {
                margin = new RectOffset(0, 4, 0, 0)
            });
            GUILayout.EndHorizontal();
            if (newChecked != active)
            {
                active = newChecked;
                if (!newChecked)
                {
                    collapse = false;
                }
            }
            if (newActive != collapse && newChecked)
            {
                collapse = newActive;
            }
            if (collapse)
            {
                GUIExtended.BeginIndent();
                UnityModManager.UI.DrawKeybinding(ref key, title);
                GUIExtended.EndIndent();
            }
        }

        public override void OnGUI()
        {
            GUIExtended.CollapsibleWithCheck(ref keyBindCollapse, ref keyBindEnabled, "키바인딩 설정", () =>
            {
                KeyMapUI(ref instantJoinKey, ref instantJoinKeyActive, ref instantJoinKeyCollapse, "cls에서 맵 바로 입장");
            }, () =>
            {
                if (keyBindEnabled)
                {
                    MiscModule.Harmony.PatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Misc.KeyBinding");
                }
                else
                {
                    MiscModule.Harmony.UnpatchConditionalTag(Assembly.GetExecutingAssembly(), "AdofaiUtils2.Misc.KeyBinding");
                }
            });
        }

        public override void Save()
        {
            SaveSettings(this);
        }
    }
}