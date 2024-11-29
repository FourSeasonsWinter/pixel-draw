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

    public void SetColor(Color color)
    {
        imageComponent.color = color;
    }

    private void OnClick(Color color)
    {
        SetColor(color);
        PaletteManager.Instance.SetActiveColorObject(gameObject);

        if (PaletteManager.Instance.IsColorPickerOpen)
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
}
