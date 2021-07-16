using System;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public class SettingsBehaviour : MonoBehaviour
    {
        public static GUIStyle Window;
        
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(200, 200, Screen.width - 400, Screen.height - 400), "와 샌즈", Window);
            GUILayout.EndArea();
        }
    }
}