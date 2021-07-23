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

        public class KeyBindingSettings
        {
            public bool Collapse;
            public bool Enabled;
            
            public class CLSSettings
            {
                // CLS 맵 입장 키
                public bool instantJoinKeyActive = true;
                public KeyBinding instantJoinKey = new KeyBinding
                {
                    keyCode = KeyCode.LeftArrow
                };
                public bool instantJoinKeyCollapse;
        
                // CLS 에서 워크샵 열기
                public bool workshopKeyActive = true;
                public KeyBinding workshopKey = new KeyBinding
                {
                    keyCode = KeyCode.W
                };
                public bool workshopKeyCollapse;
                
                // CLS 맵 목록 리로드
                public bool reloadKeyActive = true;
                public KeyBinding reloadKey = new KeyBinding
                {
                    keyCode = KeyCode.R
                };
                public bool reloadKeyCollapse;
            }

            public CLSSettings CLS = new CLSSettings();
        }

        public KeyBindingSettings KeyBinding = new KeyBindingSettings();

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
            GUIExtended.CollapsibleWithCheck(ref KeyBinding.Collapse, ref KeyBinding.Enabled, "키바인딩 설정", () =>
            {
                KeyMapUI(ref KeyBinding.CLS.instantJoinKey, ref KeyBinding.CLS.instantJoinKeyActive, ref KeyBinding.CLS.instantJoinKeyCollapse, "cls에서 맵 바로 입장");
                KeyMapUI(ref KeyBinding.CLS.workshopKey, ref KeyBinding.CLS.workshopKeyActive, ref KeyBinding.CLS.workshopKeyCollapse, "스팀 창작마당 열기");
                KeyMapUI(ref KeyBinding.CLS.reloadKey, ref KeyBinding.CLS.reloadKeyActive, ref KeyBinding.CLS.reloadKeyCollapse, "CLS 새로고침");
            }, () =>
            {
                if (KeyBinding.Enabled)
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