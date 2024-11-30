using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    private Image imageComponent;
    private Button buttonComponent;

    [SerializeField] float doubleClickTime = 1;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        buttonComponent = GetComponent<Button>();

        buttonComponent.onClick.AddListener(() => OnClick(imageComponent.color));
    }

    void Update()
    {
        if (doubleClickTime > 0)
        {
            doubleClickTime -= Time.deltaTime;
        }
    }

    private void SetColor(Color color)
    {
        imageComponent.color = color;
    }

    private void OnClick(Color color)
    {
        SetColor(color);
        PaletteManager.Instance.SetActiveColorButtonObject(gameObject);

        HandleDoubleClick();

        if (IsToDelete())
        {
            PaletteManager.Instance.DeleteColorFromPalette(gameObject);
        }
    }

    private void HandleDoubleClick()
    {
        if (PaletteManager.Instance.IsColorPickerOpen == true)
        {
            PaletteManager.Instance.ShowColorPicker();
            return;
        }

        doubleClickTime += 1;

        if (doubleClickTime > 1.5)
        {
            PaletteManager.Instance.ShowColorPicker();
            doubleClickTime = 0;
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
