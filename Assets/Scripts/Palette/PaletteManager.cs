using UnityEngine;
using UnityEngine.UI;

public class PaletteManager : MonoBehaviour
{
    [SerializeField] GameObject colorButtonPrefab;
    [SerializeField] GameObject addColorButtonObject;
    [SerializeField] GameObject paletteObject;
    [SerializeField] ColorPicker colorPicker;

    public GameObject ActiveColorButtonObject { get; private set; }
    public Color ActiveColor { get; private set; }
    public Color BackgroundColor { get; set; }

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

        SetActiveColorButtonObject(paletteObject.transform.GetChild(0).gameObject);
        colorPicker.SetColorParameters(ActiveColor);

        Color transparent = Color.white;
        transparent.a = 0;

        BackgroundColor = transparent;
    }

    public void AddColorToPalette()
    {
        GameObject newColorObject = Instantiate(colorButtonPrefab, paletteObject.transform);
        newColorObject.GetComponent<Image>().color = ActiveColor;

        addColorButtonObject.transform.SetAsLastSibling();

        SetActiveColorButtonObject(newColorObject);
        colorPicker.Show();
    }

    public void DeleteColorFromPalette(GameObject colorBtnObject)
    {
        ActiveColorButtonObject = null;
        Destroy(colorBtnObject);
    }

    public void SetActiveColorButtonObject(GameObject colorButtonObject)
    {
        Color color = colorButtonObject.GetComponent<Image>().color;
        ActiveColorButtonObject = colorButtonObject;
        colorPicker.SetColorParameters(color);
        UpdateActiveColor(color);
    }
  
    public void UpdateActiveColor(Color color)
    {
        ActiveColorButtonObject.GetComponent<Image>().color = color;
        ActiveColor = color;
    }
}
