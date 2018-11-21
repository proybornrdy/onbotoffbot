using System;
using UnityEngine;

public class XboxButton
{
    public string imgPath { get; set; }
    public Vector2 imgPosition { get; set; }
    public Vector2 imgSize { get; set; }

    public XboxButton(string path, Vector2 position, Vector2 size)
    {
        imgPath = path;
        imgPosition = position;
        imgSize = size;
    }
}