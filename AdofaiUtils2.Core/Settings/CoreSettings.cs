using System;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiUtils2.Core.Settings
{
    [Serializable]
    internal class CoreSettings : SettingsBase
    {
        public CoreSettings()
        {
            TabName = "설정";
    
            Id = "AdofaiUtils2.Core.CoreSettings";
        }

        [SerializeField]
        public KeyBinding settingsKey = new KeyBinding
        {
            keyCode = KeyCode.Comma,
            modifiers = 1
        };

        public override void Save()
        {
            SaveSettings(this);
        }

        public override void GUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Settings Key(설정 키)");
            UnityModManager.UI.DrawKeybinding(ref settingsKey, "Settings Key");
            GUILayout.EndHorizontal();
        }
    }
}