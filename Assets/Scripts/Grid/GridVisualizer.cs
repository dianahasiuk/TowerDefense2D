using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public Color gridColor = new Color(1, 1, 1, 0.1f);

    void OnDrawGizmos()
    {
        if (GridManager.Instance == null) return;
        Gizmos.color = gridColor;
        for (int x = 0; x < GridManager.Instance.width; x++)
        {
            for (int y = 0; y < GridManager.Instance.height; y++)
            {
                Vector3 pos = GridManager.Instance.GetWorldPosition(x, y);
                Gizmos.DrawWireCube(pos, Vector3.one * 0.95f);
            }
        }
    }
}