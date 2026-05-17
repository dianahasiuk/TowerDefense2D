using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Spawn Settings")]
    public EnemyData[] enemyTypes;
    public float timeBetweenEnemies = 1f;
    public int maxEnemiesPerWave = 50;

    private int enemiesDefeated;
    private int totalEnemies;
    private bool spawning = false;

    void Awake() => Instance = this;

    public void StartSpawning()
    {
        if (!spawning) StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        spawning = true;
        int round = GameManager.Instance.currentRound;
        totalEnemies = Mathf.Min(5 + (round - 1) * 2, maxEnemiesPerWave);
        enemiesDefeated = 0;

        for (int i = 0; i < totalEnemies; i++)
        {
            if (GameManager.Instance.gameOver) break;
            SpawnEnemy(round);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        yield return new WaitUntil(() => enemiesDefeated >= totalEnemies);
        spawning = false;
        GameManager.Instance.RoundComplete();
    }

    void SpawnEnemy(int round)
    {
        GameObject obj = ObjectPool.Instance.GetEnemy();
        obj.transform.position = WaypointPath.Instance.GetWaypoint(0).position;
        EnemyData data = PickEnemy(round);
        obj.GetComponent<Enemy>().Initialize(data);
    }

    EnemyData PickEnemy(int round)
    {
        if (round >= 5 && enemyTypes.Length > 2)
            return enemyTypes[Random.Range(0, enemyTypes.Length)];
        if (round >= 3 && enemyTypes.Length > 1)
            return enemyTypes[Random.Range(0, 2)];
        return enemyTypes[0];
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
    }
}