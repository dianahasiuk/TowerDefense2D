using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public int width = 12;
    public int height = 8;
    public float cellSize = 1f;

    private GridCell[,] grid;

    void Awake()
    {
        Instance = this;
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GridCell[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                grid[x, y] = new GridCell(x, y, true);
    }

    public Vector3 GetWorldPosition(int x, int y)
        => new Vector3(x * cellSize, y * cellSize, 0) + transform.position;

    public GridCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return grid[x, y];
        return null;
    }

    public void SetCellBlocked(int x, int y, bool blocked)
    {
        if (grid[x, y] != null)
            grid[x, y].isBuildable = !blocked;
    }
}

[System.Serializable]
public class GridCell
{
    public int x, y;
    public bool isBuildable;
    public bool hasTower;

    public GridCell(int x, int y, bool buildable)
    {
        this.x = x;
        this.y = y;
        this.isBuildable = buildable;
    }
}