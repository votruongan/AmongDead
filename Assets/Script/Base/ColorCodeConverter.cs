using UnityEngine;
public static class ColorCodeConverter
{
    public static Color[] colorArray = new Color[16]{
        new Color(0.28f,0.28f,0.28f),
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        new Color(0.8f,0.8f,0.8f),
        Color.magenta,
        Color.red,
        Color.white,
        new Color(0.2f,0.4f,0.0f),
        new Color(0.2f,0.4f,0.6f),
        new Color(0.2f,0.6f,0.4f),
        new Color(0.6f,0.2f,0.4f),
        new Color(0.1f,0.7f,0.3f),
        new Color(0.7f,0.1f,0.3f),
        new Color(0.7f,0.4f,0.2f)
    };
    public static Color ColorFromCode(int code)
    {
        if (code < 0 || code >= colorArray.Length) return colorArray[0];
        return colorArray[code];
    }
}