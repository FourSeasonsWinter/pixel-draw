using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteManager : MonoBehaviour
{
    public GameObject colorButtonPrefab;
    public GameObject addColorButtonObject;
    public GameObject paletteObject;
    public GameObject colorPicker;

    private List<Color> colorPalette = new();

    void Start()
    {
        UpdateColorPaletteCollection();
    }

    public void AddColorToPalette()
    {
        AddColorToPalette(Color.white);
    }

    public void AddColorToPalette(Color color)
    {
        Instantiate(colorButtonPrefab, paletteObject.transform);
        paletteObject.transform.GetChild(paletteObject.transform.childCount - 1).GetComponent<Image>().color = color;

        addColorButtonObject.transform.SetAsLastSibling();

        UpdateColorPaletteCollection();
        colorPicker.SetActive(true);
    }

    private void UpdateColorPaletteCollection()
    {
        for (int i = 0; i < paletteObject.transform.childCount - 1; ++i)
        {
            colorPalette.Add(paletteObject.transform.GetChild(i).gameObject.GetComponent<Image>().color);
        }
    }
}
