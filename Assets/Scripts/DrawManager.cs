using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public int CanvasHeight { get; private set; }
    public int CanvasWidth { get; private set; }
    public Tool SelectedTool { get; private set; }

    [SerializeField] GameObject pixelPrefab;
    [SerializeField] TMP_Text toolTextObject;
    [SerializeField] GameObject moldure;

    private const float pixelSize = 0.2f;

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

        CanvasHeight = 16;
        CanvasWidth = 16;
        SelectedTool = tools[0];

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

    private async void GenerateCanvas()
    {
        float xStartOffset = -((CanvasWidth * pixelSize) / 2);
        float xOffset = xStartOffset;
        float yOffset = CanvasHeight * pixelSize / 2;

        for (int h = 0; h < CanvasHeight; ++h)
        {
            for (int w = 0; w < CanvasWidth; ++w)
            {
                Instantiate(pixelPrefab, new Vector3(xOffset, yOffset), Quaternion.identity);
                xOffset += pixelSize;
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
