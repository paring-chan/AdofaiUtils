using System;
using UnityEngine;

namespace AdofaiUtils2.Core.Settings
{
    public static class GUIExtended
    {
        public static void BeginIndent(float size = 20.0f)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(size);
            GUILayout.BeginVertical();
        }
        
        public static void EndIndent() {
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        
        public static void CollapsibleWithCheck(ref bool active, ref bool toggleChecked, string title, Action f,
            Action onChecked)
        {
            GUILayout.BeginHorizontal();
            var newActive = GUILayout.Toggle(active, toggleChecked ? active ? "▼" : "▶" : "", new GUIStyle
            {
                fixedWidth = 10,
                margin = new RectOffset(0, 4, 0, 0),
                fontSize = 15,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            });
            var newChecked = GUILayout.Toggle(toggleChecked, title, new GUIStyle(GUI.skin.toggle)
            {
                margin = new RectOffset(0, 4, 0, 0)
            });
            GUILayout.EndHorizontal();
            if (newChecked != toggleChecked)
            {
                toggleChecked = newChecked;
                if (!newChecked)
                {
                    active = false;
                }
                onChecked.Invoke();
            }
            if (newActive != active && newChecked)
            {
                active = newActive;
                onChecked.Invoke();
            }
            if (active)
            {
                BeginIndent();
                f.Invoke();
                EndIndent();
            }
        }
    }
}