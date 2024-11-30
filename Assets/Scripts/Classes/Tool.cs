using UnityEngine;
using UnityEngine.UI;

public interface Tool
{
    public abstract string Name { get; }
    public abstract void Use(Pixel pixel);
}

public class Brush : Tool
{
    public string Name => "Brush";

    void Tool.Use(Pixel pixel)
    {
        if (PaletteManager.Instance.ActiveColorButtonObject == null) return;
        pixel.SetColor(PaletteManager.Instance.ActiveColorButtonObject.GetComponent<Image>());
    }
}

public class Eraser : Tool
{
    public string Name => "Eraser";

    public void Use(Pixel pixel)
    {
        pixel.SetColor(PaletteManager.Instance.BackgroundColor);
    }
}

public class Selector : Tool
{
    public string Name => "Selector";

    public void Use(Pixel pixel)
    {
        PaletteManager.Instance.SetActiveColorButtonObject(pixel.LinkedColorButton);
        DrawManager.Instance.ChangeSelectedTool(0);
    }
}
