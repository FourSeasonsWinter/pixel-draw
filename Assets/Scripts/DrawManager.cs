using TMPro;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Networking;
using System.Collections;

public class DrawManager : MonoBehaviour
{
    public string PixelArtName { get; private set; }
    public Tool SelectedTool { get; private set; }

    [SerializeField] TMP_Text toolTextObject;
    [SerializeField] FileFormat fileFormat = FileFormat.BMP;
    [SerializeField] CanvasManager canvas;
    [SerializeField] GameObject cameraRig;

    private readonly PixelArtExporter exporter = new();
    private readonly ConfigLoader configLoader = new();
    private readonly CloudSaver cloudSaver = new();
    private PixelArtState state;

    public static DrawManager Instance;

    private readonly Tool[] tools =
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

        PixelArtName = "Develop 2";
        SelectedTool = tools[0];

        LoadState(PixelArtName);

        canvas.Width = 16;
        canvas.Height = 16;
        canvas.Initialize();

        SetCameraBounds();

        Invoke(nameof(LoadCanvas), 0.2f);
        Invoke(nameof(LoadPalette), 0.2f);
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
        PixelArtState state = new()
        {
            name = PixelArtName,
            width = canvas.Width,
            height = canvas.Height,
            canvas = canvas.Grid,
        };
        exporter.Export(state, fileFormat);
    }

    public void SaveState()
    {
        string path = Application.persistentDataPath + $"/{PixelArtName}.json";
        PixelArtState state = new()
        {
            name = PixelArtName,
            width = canvas.Width,
            height = canvas.Height,
            canvas = canvas.Grid,
            palette = PaletteManager.Instance.GetPalette()
        };
        string json = JsonUtility.ToJson(state);

        File.WriteAllText(path, json);
    }

    public void LoadState(string pixelArtName)
    {
        string path = Application.persistentDataPath + $"/{pixelArtName}.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            state = JsonUtility.FromJson<PixelArtState>(json);
        }
    }

    public void SaveToCloud()
    {
        string url = configLoader.ApiUrl;
        StartCoroutine(cloudSaver.PostSaveData(url, state));
    }

    public void LoadFromCloud()
    {
        string url = configLoader.ApiUrl + $"/{PixelArtName}";
        StartCoroutine(cloudSaver.GetSaveData(url, state));

        LoadCanvas();
        LoadPalette();
    }

    private void LoadCanvas()
    {
        if (state == null) return;
        canvas.SetGridColors(state.canvas);
    }

    private void LoadPalette()
    {
        if (state == null) return;
        if (state.palette.Length == 0)
        {
            PaletteManager.Instance.AddColor(new Color(0, 1, 232 / 255));
        }

        foreach (var color in state.palette)
        {
            PaletteManager.Instance.AddColor(color);
        }
    }

    private void SetCameraBounds()
    {
        cameraRig.GetComponent<CameraController>().SetBounds(canvas.Width * 0.2f / 2, canvas.Height * 0.2f / 2);
    }
}