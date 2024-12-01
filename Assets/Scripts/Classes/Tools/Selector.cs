
public class Selector : Tool
{
    public string Name => "Selector";

    public void Use(Pixel pixel)
    {
        if (pixel.LinkedColorButton != null)
        {
            PaletteManager.Instance.SetActiveColorButtonObject(pixel.LinkedColorButton);
            DrawManager.Instance.ChangeSelectedTool(Tools.Brush);
        }
    }
}
