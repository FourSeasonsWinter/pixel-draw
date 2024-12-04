using UnityEngine;

[System.Serializable]
public class PixelArtState
{
    public string name;
    public int width;
    public int height;
    public Color[] canvas;
    public Color[] palette;

    public PixelArtState(string name, int width, int height, Color[] canvas, Color[] palette)
    {
        this.name = name;
        this.width = width;
        this.height = height;
        this.canvas = canvas;
        this.palette = palette;
    }
}
