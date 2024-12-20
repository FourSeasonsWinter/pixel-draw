using System.Collections;
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

    private void SetColor(Color color)
    {
        imageComponent.color = color;
    }

    private void OnClick(Color color)
    {
        SetColor(color);
        PaletteManager.Instance.SetActiveColorButtonObject(gameObject);

        if (IsToDelete())
        {
            PaletteManager.Instance.DeleteColorFromPalette(gameObject);
        }
    }

    private bool IsToDelete()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }

        return false;
    }
}
