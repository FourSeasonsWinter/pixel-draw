using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteManager : MonoBehaviour
{
    public GameObject paletteObject;

    private List<Color> colorPalette = new();

    void Start()
    {
        for (int i = 0; i < paletteObject.transform.childCount; ++i)
        {
            colorPalette.Add(paletteObject.transform.GetChild(i).gameObject.GetComponent<Image>().color);
        }

        Debug.Log($"Colors in palette: {colorPalette.Count}");
    }

    public bool IsColorInPalette(Color color)
    {
        foreach (var c in colorPalette)
        {
            if (c == color) return true;
        }

        return false;
    }

    public void UpdatePalette()
    {
    }
}
