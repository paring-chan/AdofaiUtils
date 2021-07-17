using System;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsUI : MonoBehaviour
    {
        public static bool Open;
        public static bool Escape;
        private Vector2 tabScrollPosition;
        private string current;

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Comma) && !Open)
            {
                Open = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && Open)
            {
                Escape = true;
                Open = false;
            }
        }

        private void OnGUI()
        {
            if (!Open) return;
            GUI.skin = Assets.GUISkin;
            GUILayout.BeginArea(new Rect(50, 50, Screen.width - 100, Screen.height - 100), "AdofaiUtils2 설정", GUI.skin.window);

            tabScrollPosition = GUILayout.BeginScrollView(tabScrollPosition, GUILayout.MaxHeight(55.0f));
            GUILayout.BeginHorizontal();

            foreach (var v in SettingsManager.SettingsMap)
            {
                var settings = v.Value;
                if (GUILayout.Button(settings.TabName))
                {
                    current = settings.Id;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            if (!SettingsManager.SettingsMap.TryGetValue(current ?? "AdofaiUtils2.Core.CoreSettings", out var guiSettings))
            {
                GUILayout.Label("설정을 선택해주세요.");
            }
            else
            {
                guiSettings.GUI();
            }
            GUILayout.EndArea();
        }
    }
}