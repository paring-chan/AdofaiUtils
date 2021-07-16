using System;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsBehaviour : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.skin = Assets.GUISkin;
            GUILayout.BeginArea(new Rect(50, 50, Screen.width - 100, Screen.height - 100), "와 샌즈", GUI.skin.window);
            GUILayout.Button("와아");
            GUILayout.EndArea();
        }
    }
}