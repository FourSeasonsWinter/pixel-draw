using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private Image imageComponent;
    private Button buttonComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        buttonComponent = GetComponent<Button>();

        buttonComponent.onClick.AddListener(() => OnClick(imageComponent.color));
    }

    public void SetColor(Color color)
    {
        imageComponent.color = color;
    }

    private void OnClick(Color color)
    {
        SetColor(color);
        PaletteManager.Instance.SetActiveColorObject(gameObject);
        PaletteManager.Instance.ShowColorPicker();
    }
}
