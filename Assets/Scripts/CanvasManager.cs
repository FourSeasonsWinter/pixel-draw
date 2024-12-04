using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private int width;
    private int height;

    public int Width
    {
        get { return width; }
        set { width = CheckDimensionValue(value); }
    }
    public int Height
    {
        get { return height; }
        set { height = CheckDimensionValue(value); }
    }

    [SerializeField] GameObject pixelPrefab;
    [SerializeField] GameObject moldure;

    private const float pixelSize = 0.2f;

    public void Initialize()
    {
        GenerateCanvas();
        GenerateMoldure();
    }

    public Color[,] GetPixelsColors()
    {
        Color[,] colors = new Color[width, height];
        Transform pixelsContainer = GameObject.Find("Pixels").transform;
        int index = 0;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Pixel pixel = pixelsContainer.GetChild(index).GetComponent<Pixel>();
                colors[x, y] = pixel.Color;
                index++;
            }
        }

        return colors;
    }

    public Color[] GetGridColors()
    {
        Color[] colors = new Color[width * height];
        Transform pixelsContainer = GameObject.Find("Pixels").transform;

        for (int i = 0; i < pixelsContainer.childCount; ++i)
        {
            colors[i] = pixelsContainer.GetChild(i).GetComponent<Pixel>().Color;
        }

        return colors;
    }

    public void SetGridColors(Color[] colors)
    {

    }

    private async void GenerateCanvas()
    {
        float xStartOffset = -(width * pixelSize / 2);
        float xOffset = xStartOffset;
        float yOffset = height * pixelSize / 2;
        Transform pixelsContainer = GameObject.Find("Pixels").transform;
        int id = 0;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
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
        float moldureSize = width * pixelSize;
        Vector3 targetScale = new(moldureSize, moldureSize);
        moldure.transform.localScale = targetScale;
    }

    private int CheckDimensionValue(int value)
    {
        if (value < 0)
        {
            return 1;
        }
        else if (value > 64)
        {
            return 64;
        }
        else
        {
            return value;
        }
    }
}
