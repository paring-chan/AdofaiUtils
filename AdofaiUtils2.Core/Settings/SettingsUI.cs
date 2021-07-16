using System;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        public static bool Open = true;

        private void OnGUI()
        {
            if (!Open) return;
            GUI.skin = Assets.GUISkin;
            GUILayout.BeginArea(new Rect(50, 50, Screen.width - 100, Screen.height - 100), "와아아아", GUI.skin.window);
            GUILayout.Button("와아");
            GUILayout.EndArea();
        }
    }
}