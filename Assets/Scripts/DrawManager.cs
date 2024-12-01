using System.Collections;
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

        CanvasHeight = 8;
        CanvasWidth = 8;
        SelectedTool = tools[0];

        StartCoroutine(GenerateCanvas());
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

    private IEnumerator GenerateCanvas()
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
            }

            xOffset = xStartOffset;
            yOffset -= pixelSize;
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void GenerateMoldure()
    {
        float xStart = -((CanvasWidth * pixelSize) - 1.5f);
        float yStart = CanvasHeight * pixelSize - 1.5f;
        float moldureSize = (CanvasWidth * pixelSize) + 0.1f;

        Vector3 targetScale = new(moldureSize, moldureSize);
        Vector3 targetPosition = new(xStart, yStart);

        moldure.transform.localScale = targetScale;
        moldure.transform.position = targetPosition;
    }
}
