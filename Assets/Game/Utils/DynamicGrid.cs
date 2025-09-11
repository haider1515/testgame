using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGrid : MonoBehaviour
{
    public int rows = 4;
    public int cols = 4;
    public Vector2 spacing = new Vector2(10, 10);
    public Vector2 padding = new Vector2(20, 20);

    private GridLayoutGroup grid;
    private RectTransform rect;

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        rect = GetComponent<RectTransform>();
        UpdateLayout();
    }

    void OnRectTransformDimensionsChange()
    {
        UpdateLayout();
    }

    public void UpdateLayout()
    {
        if (rows <= 0 || cols <= 0) return;

        float width = rect.rect.width - padding.x - spacing.x * (cols - 1);
        float height = rect.rect.height - padding.y - spacing.y * (rows - 1);

        float cellW = width / cols;
        float cellH = height / rows;

        float size = Mathf.Min(cellW, cellH);

        grid.cellSize = new Vector2(size, size);
        grid.spacing = spacing;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;
    }
}
