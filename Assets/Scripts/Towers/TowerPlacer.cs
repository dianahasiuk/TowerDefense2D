using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer Instance;

    [Header("Tower Prefab")]
    public GameObject towerPrefab;

    private TowerData selectedTower;
    private bool isPlacing = false;

    void Awake() => Instance = this;

    public void SelectTower(TowerData data)
    {
        selectedTower = data;
        isPlacing = true;
    }

    void Update()
    {
        if (!isPlacing) return;
        if (Input.GetMouseButtonDown(0))
            TryPlaceTower();
        if (Input.GetMouseButtonDown(1))
            isPlacing = false;
    }

    void TryPlaceTower()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        int x = Mathf.RoundToInt(mousePos.x);
        int y = Mathf.RoundToInt(mousePos.y);

        GridCell cell = GridManager.Instance.GetCell(x, y);
        if (cell == null || !cell.isBuildable || cell.hasTower) return;
        if (!GameManager.Instance.SpendGold(selectedTower.cost)) return;

        GameObject obj = Instantiate(towerPrefab, new Vector3(x, y, 0), Quaternion.identity);
        obj.GetComponent<Tower>().Initialize(selectedTower);
        cell.hasTower = true;
        isPlacing = false;
    }
}