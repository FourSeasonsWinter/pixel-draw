using UnityEngine;
using UnityEngine.UI;

public class Pixel : MonoBehaviour
{
    private SpriteRenderer spriteRendererComponent;
    private Image colorObjectImage;
    private Color savedColor = Color.white;

    void Start()
    {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (colorObjectImage == null)
        {
            spriteRendererComponent.color = savedColor;
            return;
        }

        spriteRendererComponent.color = colorObjectImage.color;
        savedColor = colorObjectImage.color;
    }

    public void SetColor(Image image)
    {
        colorObjectImage = image;
    }

    public void SetColor(Color color)
    {
        colorObjectImage = null;
        savedColor = color;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            DrawManager.Instance.SelectedTool.Use(this);
        }
    }
}
