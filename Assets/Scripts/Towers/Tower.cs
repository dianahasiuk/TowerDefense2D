using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector] public TowerData data;
    private float fireCountdown = 0f;
    private Enemy target;

    public void Initialize(TowerData towerData)
    {
        data = towerData;
        if (data.sprite != null)
            GetComponent<SpriteRenderer>().sprite = data.sprite;
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.Battle) return;
        FindBestTarget();
        if (target == null) return;
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / data.fireRate;
        }
    }

    void FindBestTarget()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float bestProgress = -1f;
        Enemy best = null;

        foreach (Enemy e in enemies)
        {
            if (!e.gameObject.activeSelf) continue;
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist <= data.range && e.GetPathProgress() > bestProgress)
            {
                bestProgress = e.GetPathProgress();
                best = e;
            }
        }
        target = best;
    }

    void Shoot()
    {
        if (data.projectilePrefab == null) return;
        GameObject proj = Instantiate(data.projectilePrefab,
            transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>()?.Initialize(target, data.damage, data.type);
    }

    void OnDrawGizmosSelected()
    {
        if (data == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}