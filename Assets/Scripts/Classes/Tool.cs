using UnityEngine;
using UnityEngine.UI;

public interface Tool
{
    public string Name { get; protected set; }

    public void Use(Pixel pixel);
}

public class Brush : Tool
{
    string Tool.Name { get => "Brush"; set => throw new System.NotImplementedException(); }

    void Tool.Use(Pixel pixel)
    {
        if (PaletteManager.Instance.ActiveColorButtonObject == null) return;
        pixel.SetColor(PaletteManager.Instance.ActiveColorButtonObject.GetComponent<Image>());
    }
}
