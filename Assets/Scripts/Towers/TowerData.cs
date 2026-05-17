using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TowerDefense/Tower Data")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public int cost;
    public float damage;
    public float fireRate;
    public float range;
    public TowerType type;
    public Sprite sprite;
    public GameObject projectilePrefab;
}

public enum TowerType
{
    Arrow,
    Cannon,
    Freeze,
    Sniper
}