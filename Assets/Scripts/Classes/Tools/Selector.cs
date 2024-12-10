
public class Selector : Tool
{
    public string Name => "Selector";

    public void Use(Pixel pixel)
    {
        if (pixel.LinkedColorButton != null)
        {
            PaletteManager.Instance.SetActiveColorButtonObject(pixel.LinkedColorButton);
        }
        else
        {
            PaletteManager.Instance.UpdateActiveColor(pixel.Color);
        }
    }
}
