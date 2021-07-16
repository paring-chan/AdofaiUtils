using System;
using UnityEngine;

public class GUISkinTest : MonoBehaviour
{
    public GUISkin skin;

    private Vector2 _pos = new Vector2(0, 0);
    
    private void OnGUI()
    {
        GUI.skin = skin;
        
        GUILayout.BeginArea(new Rect(50,50, Screen.width-100,Screen.height-100), "와아아", GUI.skin.window);

        // GUI.Button(new Rect(50,50,300, 50), "버튼");

        _pos = GUILayout.BeginScrollView(_pos);

        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        GUILayout.Button("와아아");
        
        GUILayout.EndScrollView();
        
        GUILayout.EndArea();
    }
}
