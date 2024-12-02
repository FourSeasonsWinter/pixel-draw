using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public int CanvasHeight { get; private set; }
    public int CanvasWidth { get; private set; }

    [SerializeField] GameObject pixelPrefab;
    [SerializeField] GameObject moldure;

    private const float pixelSize = 0.2f;
    private Color[] savedState;

    public static CanvasManager Instance;

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

        CanvasHeight = 16;
        CanvasWidth = 16;
        savedState = new Color[CanvasWidth * CanvasHeight];

        GenerateCanvas();
        GenerateMoldure();
    }

    void Update()
    {
        
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

    public Color[,] GetPixelsColors()
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
}
