using UnityEngine;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image colorObjectImage;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (colorObjectImage == null) return;
        spriteRenderer.color = colorObjectImage.color;
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            colorObjectImage = PaletteManager.Instance.ActiveColorObject.GetComponent<Image>();
        }
    }
}
