using System;
using UnityEngine;

public class GUISkinTest : MonoBehaviour
{
    public GUISkin skin;

    private Vector2 _pos = new Vector2(0, 0);

    private bool _check;

    private void OnGUI()
    {
        float DesignWidth = 640.0f;
        float DesignHeight = 360.0f;
        //
        // //Calculate change aspects
        float resX = (float) (Screen.width) / DesignWidth;
        
        float resY = (float) (Screen.height) / DesignHeight;

        //Set matrix
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));
        // //Draw GUI elements
        // //Button (Size: 200x150) in center define screen

        GUI.skin = skin;

        GUILayout.BeginArea(new Rect(50, 50, DesignWidth - 100, DesignHeight - 100), "와아아", GUI.skin.window);

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

        _check = GUILayout.Toggle(_check, "체크");

        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }
}