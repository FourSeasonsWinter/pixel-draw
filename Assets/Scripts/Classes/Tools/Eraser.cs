
public class Eraser : Tool
{
    public string Name => "Eraser";

    public void Use(Pixel pixel)
    {
        pixel.SetColor(PaletteManager.Instance.BackgroundColor);
    }
}
