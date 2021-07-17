using System;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        public static bool Open = true;
        public static bool Escape;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Comma) && !Open)
            {
                Open = true;
            } else if (Input.GetKeyDown(KeyCode.Escape) && Open)
            {
                Escape = true;
                Open = false;
            }
        }

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