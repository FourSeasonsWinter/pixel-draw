using UnityEngine;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image colorObjectImage;
    private Color savedColor = Color.white;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (colorObjectImage == null)
        {
            spriteRenderer.color = savedColor;
            return;
        }

        spriteRenderer.color = colorObjectImage.color;
        savedColor = colorObjectImage.color;
    }

    public Color GetColor()
    {
        return savedColor;
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (PaletteManager.Instance.ActiveColorButtonObject == null) return;

            colorObjectImage = PaletteManager.Instance.ActiveColorButtonObject.GetComponent<Image>();
        }
    }
}
