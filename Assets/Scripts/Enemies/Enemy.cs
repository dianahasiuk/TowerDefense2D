using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyData data;

    private float currentHealth;
    private int waypointIndex = 0;
    private float currentSpeed;
    private bool isFrozen = false;

    [Header("Healthbar")]
    public Image healthbarFill;

    public void Initialize(EnemyData enemyData)
    {
        data = enemyData;
        currentHealth = data.maxHealth;
        currentSpeed = data.speed;
        waypointIndex = 0;
        UpdateHealthbar();
        if (data.sprite != null)
            GetComponent<SpriteRenderer>().sprite = data.sprite;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (WaypointPath.Instance == null) return;
        if (waypointIndex >= WaypointPath.Instance.GetWaypointCount())
        {
            ReachBase();
            return;
        }
        Transform target = WaypointPath.Instance.GetWaypoint(waypointIndex);
        transform.position = Vector3.MoveTowards(
            transform.position, target.position, currentSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
            waypointIndex++;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthbar();
        if (currentHealth <= 0) Die();
    }

    void UpdateHealthbar()
    {
        if (healthbarFill)
            healthbarFill.fillAmount = currentHealth / data.maxHealth;
    }

    public void ApplyFreeze(float duration, float slowFactor)
    {
        if (data.immuneToFreeze) return;
        if (!isFrozen) StartCoroutine(FreezeRoutine(duration, slowFactor));
    }

    System.Collections.IEnumerator FreezeRoutine(float duration, float slow)
    {
        isFrozen = true;
        currentSpeed = data.speed * slow;
        yield return new WaitForSeconds(duration);
        currentSpeed = data.speed;
        isFrozen = false;
    }

    void ReachBase()
    {
        GameManager.Instance.DamageBase(data.baseDamage);
        ObjectPool.Instance.ReturnEnemy(gameObject);
    }

    void Die()
    {
        GameManager.Instance.AddGold(data.goldReward);
        EnemySpawner.Instance.EnemyDefeated();
        ObjectPool.Instance.ReturnEnemy(gameObject);
    }

    public float GetPathProgress() => waypointIndex;
}