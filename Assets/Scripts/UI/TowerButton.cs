using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public TowerData towerData;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        TowerPlacer.Instance.SelectTower(towerData);
    }
}