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

        StartCoroutine(GenerateTheCanvas());

        PaletteManager.Instance.BackgroundColor = Color.white;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SelectedTool = tools[0];
            toolTextObject.text = SelectedTool.Name;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectedTool = tools[1];
            toolTextObject.text = SelectedTool.Name;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectedTool = tools[2];
            toolTextObject.text = SelectedTool.Name;
        }
    }

    public void ChangeSelectedTool(int toolIndex)
    {
        SelectedTool = tools[toolIndex];
        toolTextObject.text = SelectedTool.Name;
    }

    private IEnumerator GenerateTheCanvas()
    {
        float xStartOffset = -((CanvasWidth * 0.2f) / 2);
        float xOffset = xStartOffset;
        float yOffset = -((CanvasHeight * 0.2f) / 2);

        for (int h = 0; h < CanvasHeight; ++h)
        {
            for (int w = 0; w < CanvasWidth; ++w)
            {
                Instantiate(pixelPrefab, new Vector3(xOffset, yOffset), Quaternion.identity);
                xOffset += 0.2f;
            }

            xOffset = xStartOffset;
            yOffset += 0.2f;
        }

        yield return new WaitForSeconds(0.1f);
    }
}
