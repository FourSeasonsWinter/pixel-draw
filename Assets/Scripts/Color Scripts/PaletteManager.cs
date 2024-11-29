using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteManager : MonoBehaviour
{
    public GameObject colorButtonPrefab;
    public GameObject addColorButtonObject;
    public GameObject paletteObject;
    public ColorPicker colorPicker;

    private List<Color> colorPalette = new();

    public GameObject ActiveColorObject { get; private set; }
    public Color ActiveColor { get; private set; }
    public bool IsColorPickerOpen { get; private set; }
    public static PaletteManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetActiveColorObject(paletteObject.transform.GetChild(paletteObject.transform.childCount - 1).gameObject);
        UpdateColorPaletteCollection();
        colorPicker.Hide();
    }

    public void AddColorToPalette()
    {
        AddColorToPalette(Color.white);
    }

    public void AddColorToPalette(Color color)
    {
        GameObject newColorObject = Instantiate(colorButtonPrefab, paletteObject.transform);
        newColorObject.GetComponent<Image>().color = color;

        addColorButtonObject.transform.SetAsLastSibling();

        UpdateColorPaletteCollection();
        SetActiveColorObject(newColorObject);
    }

    public void SetActiveColorObject(GameObject colorObject)
    {
        ActiveColorObject = colorObject;
        UpdateActiveColor(colorObject.GetComponent<Image>().color);
    }
  
    public void UpdateActiveColor(Color color)
    {
        ActiveColorObject.GetComponent<Image>().color = color;
        ActiveColor = color;
    }

    public void ShowColorPicker()
    {
        colorPicker.Show();
        colorPicker.SetColorParameters(ActiveColor);
        IsColorPickerOpen = true;
    }

    public void HideColorPicker()
    {
        colorPicker.Hide();
        IsColorPickerOpen = false;
    }

    private void UpdateColorPaletteCollection()
    {
        for (int i = 0; i < paletteObject.transform.childCount - 1; ++i)
        {
            colorPalette.Add(paletteObject.transform.GetChild(i).gameObject.GetComponent<Image>().color);
        }
    }
}
