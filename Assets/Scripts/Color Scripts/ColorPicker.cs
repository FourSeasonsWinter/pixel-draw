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

        hexadecimalField.onValueChanged.AddListener(UpdateColor);
        hexadecimalField.textComponent.color = Color.white;

        UpdateColor(0.0f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void UpdateColor(float value)
    {
        Color newColor = new(redSlider.value, greenSlider.value, blueSlider.value, alphaSlider.value);

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

    private void UpdateColor(string hexValue)
    {
        Debug.Log("Hex update color");
    }
}
