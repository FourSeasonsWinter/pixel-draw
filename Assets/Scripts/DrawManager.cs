using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DrawManager : MonoBehaviour
{
    public string PixelArtName { get; private set; }
    public int CanvasHeight { get; private set; }
    public int CanvasWidth { get; private set; }
    public Tool SelectedTool { get; private set; }

    [SerializeField] GameObject pixelPrefab;
    [SerializeField] TMP_Text toolTextObject;
    [SerializeField] GameObject moldure;

    private const float pixelSize = 0.2f;
    private const string fileExtension = ".png";
    private Color[] savedState;

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
        CanvasHeight = 16;
        CanvasWidth = 16;
        SelectedTool = tools[0];
        savedState = new Color[CanvasWidth * CanvasHeight];

        GenerateCanvas();
        GenerateMoldure();
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

    public void SaveCanvas()
    {
        Pixel[,] pixelsColors = GetPixels();

        foreach (var pixel in pixelsColors)
        {
            savedState[pixel.Id] = pixel.Color;
        }
    }

    public void ReloadCanvas()
    {
        Pixel[,] pixels = GetPixels();

        foreach (var pixel in pixels)
        {
            pixel.SetColor(savedState[pixel.Id]);
        }
    }

    public void Export()
    {
        PixelArtExporter.Export(GetPixelsColors(), PixelArtName + fileExtension);
    }

    private Pixel[,] GetPixels()
    {
        Pixel[,] pixels = new Pixel[CanvasWidth, CanvasHeight];
        Transform pixelsContainer = GameObject.Find("Pixels").transform;
        int index = 0;

        for (int y = 0; y < CanvasHeight; ++y)
        {
            for (int x = 0; x < CanvasWidth; ++x)
            {
                Pixel pixel = pixelsContainer.GetChild(index).gameObject.GetComponent<Pixel>();
                pixels[x, y] = pixel;
                index++;
            }
        }

        return pixels;
    }

    private Color[,] GetPixelsColors()
    {
        Color[,] colors = new Color[CanvasWidth, CanvasHeight];
        Pixel[,] pixels = GetPixels();

        int x = 0;
        int y = 0;

        foreach (var pixel in pixels)
        {
            colors[x, y] = pixel.Color;
            x++;

            if (x == CanvasWidth)
            {
                x = 0;
                y++;
            }
        }

        return colors;
    }

    private async void GenerateCanvas()
    {
        float xStartOffset = -(CanvasWidth * pixelSize / 2);
        float xOffset = xStartOffset;
        float yOffset = CanvasHeight * pixelSize / 2;
        Transform pixelsContainer = GameObject.Find("Pixels").transform;
        int id = 0;

        for (int y = 0; y < CanvasHeight; ++y)
        {
            for (int x = 0; x < CanvasWidth; ++x)
            {
                GameObject pixel = Instantiate(pixelPrefab, new Vector3(xOffset, yOffset), Quaternion.identity, pixelsContainer);
                pixel.GetComponent<Pixel>().Id = id;
                
                xOffset += pixelSize;
                id += 1;
                await Task.Delay((int)0.01f);
            }

            xOffset = xStartOffset;
            yOffset -= pixelSize;
        }
    }

    private void GenerateMoldure()
    {
        float moldureSize = CanvasWidth * pixelSize;
        Vector3 targetScale = new(moldureSize, moldureSize);
        moldure.transform.localScale = targetScale;
    }
}
