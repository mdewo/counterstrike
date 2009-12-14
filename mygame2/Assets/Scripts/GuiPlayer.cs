﻿using UnityEngine;
using System.Collections;


public class GuiPlayer : Base
{
    protected override void OnGUI()
    {
        float w = Screen.width;
        float h = Screen.height;
        Rect r = Rect.MinMaxRect(w - w / 4, h - h / 5, Screen.width, Screen.height);
        GUILayout.Window(0, r, DrawWind, "");
    }
    Player player { get { return Find<Player>("LocalPlayer"); } }
    public void DrawWind(int q)
    {

    }
}