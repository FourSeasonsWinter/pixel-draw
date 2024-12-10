using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private Slider redSlider;
    private Slider greenSlider;
    private Slider blueSlider;
    private Slider alphaSlider;
    private TMP_Text redText;
    private TMP_Text greenText;
    private TMP_Text blueText;
    private TMP_Text alphaText;

    public Image colorDisplay;
    public TMP_InputField hexadecimalField;

    void Start()
    {
        redSlider = GameObject.Find("Red Slider").GetComponent<Slider>();
        greenSlider = GameObject.Find("Green Slider").GetComponent<Slider>();
        blueSlider = GameObject.Find("Blue Slider").GetComponent<Slider>();
        alphaSlider = GameObject.Find("Alpha Slider").GetComponent<Slider>();

        redText = GameObject.Find("Red Text").GetComponent<TMP_Text>();
        greenText = GameObject.Find("Green Text").GetComponent<TMP_Text>();
        blueText = GameObject.Find("Blue Text").GetComponent<TMP_Text>();
        alphaText = GameObject.Find("Alpha Text").GetComponent<TMP_Text>();

        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);
        alphaSlider.onValueChanged.AddListener(UpdateColor);

        hexadecimalField.textComponent.color = Color.white;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetColorParameters(Color color)
    {
        redSlider.value = color.r;
        greenSlider.value = color.g;
        blueSlider.value = color.b;
        alphaSlider.value = color.a;
    }

    public void OnHexadecimalEnter()
    {
        string hex = hexadecimalField.text;
        Color color = GetColorFromHex(hex);

        redSlider.value = color.r;
        greenSlider.value = color.g;
        blueSlider.value = color.b;

        UpdateColor(0);
    }

    private void UpdateColor(Single _)
    {
        Color newColor = new(redSlider.value, greenSlider.value, blueSlider.value, alphaSlider.value);
        PaletteManager.Instance.UpdateActiveColor(newColor);

        redText.text = (redSlider.value * 255).ToString("F0");
        greenText.text = (greenSlider.value * 255).ToString("F0");
        blueText.text = (blueSlider.value * 255).ToString("F0");
        alphaText.text = (alphaSlider.value * 255).ToString("F0");
        hexadecimalField.text = "#" + newColor.ToHexString();
        colorDisplay.color = newColor;

        if (alphaText.text == "255")
        {
            hexadecimalField.text = hexadecimalField.text[..7];
        }
    }

    private Color GetColorFromHex(string hex)
    {
        if (hex[0] == '#')
        {
            hex = hex[1..];
        }

        if (hex.Length == 6)
        {
            hex = "FF" + hex;
        }
        if (hex.Length != 8)
        {
            throw new System.ArgumentException("Hex string must be 6 or 8 characters long");
        }

        byte a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte r = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, a);
    }
}
