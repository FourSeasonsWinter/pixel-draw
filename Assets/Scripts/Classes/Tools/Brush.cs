using UnityEngine.UI;

public class Brush : Tool
{
    public string Name => "Brush";

    void Tool.Use(Pixel pixel)
    {
        if (PaletteManager.Instance.ActiveColorButtonObject == null) return;
        pixel.SetColor(PaletteManager.Instance.ActiveColorButtonObject.GetComponent<Image>());
    }
}
