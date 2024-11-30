using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ColorPaletteResizer : MonoBehaviour
{
    private GridLayoutGroup gridLayoutComponent;
    private RectTransform rectTransformComponent;
    [SerializeField] int columns = 4;

    void Start()
    {
        gridLayoutComponent = GetComponent<GridLayoutGroup>();
        rectTransformComponent = GetComponent<RectTransform>();
    }

    void Update()
    {
        ResizeGrid(columns);
    }

    public void ResizeGrid(int columns)
    {
        this.columns = columns;
        gridLayoutComponent.constraintCount = columns;
    }
}
