using UnityEngine;

public class Pixel : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            spriteRenderer.color = PaletteManager.Instance.ActiveColor;
        }
    }
}
