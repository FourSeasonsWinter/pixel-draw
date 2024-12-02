using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DrawManager : MonoBehaviour
{
    public string PixelArtName { get; private set; }
    public Tool SelectedTool { get; private set; }

    [SerializeField] TMP_Text toolTextObject;
    [SerializeField] FileFormat selectedFormat = FileFormat.BMP;

    private PixelArtExporter exporter = new();

    public static DrawManager Instance;

    private Tool[] tools =
    {
        new Brush(),
        new Eraser(),
        new Selector()
    };

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

        PixelArtName = "Develop";
        SelectedTool = tools[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SelectedTool = tools[(int)Tools.Brush];
            toolTextObject.text = SelectedTool.Name;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectedTool = tools[(int)Tools.Eraser];
            toolTextObject.text = SelectedTool.Name;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectedTool = tools[(int)Tools.Selector];
            toolTextObject.text = SelectedTool.Name;
        }
    }

    public void ChangeSelectedTool(Tools tool)
    {
        SelectedTool = tools[(int)tool];
        toolTextObject.text = SelectedTool.Name;
    }

    public void Export()
    {
        exporter.Export(CanvasManager.Instance.GetPixelsColors(), PixelArtName, selectedFormat);
    }
}
