using UnityEngine;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    private SpriteRenderer spriteRendererComponent;
    private Image colorObjectImage;

    public Color Color { get; private set; }
    public GameObject LinkedColorButton { get; private set; }
    public int Id { get; set; }

    void Start()
    {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        Color = PaletteManager.Instance.BackgroundColor;
    }

    void Update()
    {
        if (colorObjectImage == null)
        {
            spriteRendererComponent.color = Color;
            return;
        }

        spriteRendererComponent.color = colorObjectImage.color;
        Color = colorObjectImage.color;
    }

    public void SetColor(Image image)
    {
        colorObjectImage = image;
        LinkedColorButton = image.gameObject;
    }

    public void SetColor(Color color)
    {
        colorObjectImage = null;
        Color = color;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            DrawManager.Instance.SelectedTool.Use(this);
        }
    }
}