using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteManager : MonoBehaviour
{
    [SerializeField] GameObject colorButtonPrefab;
    [SerializeField] GameObject addColorButtonObject;
    [SerializeField] GameObject paletteContainer;
    [SerializeField] ColorPicker colorPicker;

    public GameObject ActiveColorButtonObject { get; private set; }
    public Color BackgroundColor { get; set; }

    public static PaletteManager Instance;

    private Color activeColor;

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

        SetActiveColorButtonObject(paletteContainer.transform.GetChild(0).gameObject);

        Color transparent = Color.white;
        transparent.a = 0;

        BackgroundColor = transparent;
    }

    public void AddColor(Color color)
    {
        activeColor = color;
        AddColorToPalette();
    }

    public void AddColorToPalette()
    {
        GameObject newColorObject = Instantiate(colorButtonPrefab, paletteContainer.transform);
        newColorObject.GetComponent<Image>().color = activeColor;

        addColorButtonObject.transform.SetAsLastSibling();

        SetActiveColorButtonObject(newColorObject);
    }

    public void DeleteColorFromPalette(GameObject colorBtnObject)
    {
        ActiveColorButtonObject = null;
        Destroy(colorBtnObject);
    }

    public void SetActiveColorButtonObject(GameObject colorButtonObject)
    {
        ActiveColorButtonObject = colorButtonObject;

        Color color = colorButtonObject.GetComponent<Image>().color;
        colorPicker.SetColorParameters(color);
        UpdateActiveColor(color);
    }
  
    public void UpdateActiveColor(Color color)
    {
        ActiveColorButtonObject.GetComponent<Image>().color = color;
        activeColor = color;
    }

    public Color[] GetPalette()
    {
        int colorAmount = paletteContainer.transform.childCount - 1;
        Color[] palette = new Color[colorAmount];

        for (int i = 0; i < colorAmount; ++i)
        {
            palette[i] = paletteContainer.transform.GetChild(i).GetComponent<Image>().color;
        }

        return palette;
    }
}
