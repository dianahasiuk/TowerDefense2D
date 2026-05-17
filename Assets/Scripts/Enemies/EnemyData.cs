using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TowerDefense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float speed;
    public int goldReward;
    public int baseDamage;
    public bool immuneToFreeze;
    public Sprite sprite;
}