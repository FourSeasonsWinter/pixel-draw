using TMPro;
using UnityEngine;
using System.IO;

public class DrawManager : MonoBehaviour
{
    public string PixelArtName { get; private set; }
    public Tool SelectedTool { get; private set; }

    [SerializeField] TMP_Text toolTextObject;
    [SerializeField] FileFormat selectedFormat = FileFormat.BMP;
    [SerializeField] CanvasManager canvas;
    [SerializeField] GameObject cameraRig;

    private readonly PixelArtExporter exporter = new();
    private PixelArtState state;

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

        LoadState(PixelArtName);

        canvas.Width = 16;
        canvas.Height = 16;
        canvas.Initialize();

        SetCameraBounds();
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
        exporter.Export(canvas.GetPixelsColors(), PixelArtName, selectedFormat);
    }

    public void SaveState()
    {
        state = new PixelArtState(PixelArtName, canvas.Width, canvas.Height, canvas.GetGridColors(), new Color[1]);
        string json = JsonUtility.ToJson(state);
        string path = Application.persistentDataPath + $"/{PixelArtName}.json";
        File.WriteAllText(path, json);

        Debug.Log($"Json saved to {path}.");
        Debug.Log($"Json: {json}");
    }

    public void LoadState(string pixelArtName)
    {
        string path = Application.persistentDataPath + $"/{pixelArtName}.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            state = JsonUtility.FromJson<PixelArtState>(json);
        }
        else
        {
            Debug.Log("No saved state found.");
        }
    }

    private void LoadCanvas()
    {
        if (state == null) return;


    }

    private void SetCameraBounds()
    {
        cameraRig.GetComponent<CameraController>().SetBounds(canvas.Width * 0.2f / 2, canvas.Height * 0.2f / 2);
    }
}
