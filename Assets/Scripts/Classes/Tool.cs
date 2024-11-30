using UnityEngine;
using UnityEngine.UI;

public interface Tool
{
    public abstract void Use(Pixel pixel);
}

public class Brush : Tool
{
    void Tool.Use(Pixel pixel)
    {
        if (PaletteManager.Instance.ActiveColorButtonObject == null) return;
        pixel.SetColor(PaletteManager.Instance.ActiveColorButtonObject.GetComponent<Image>());
    }
}

public class Eraser : Tool
{
    public void Use(Pixel pixel)
    {
        pixel.SetColor(PaletteManager.Instance.BackgroundColor);
    }
}
