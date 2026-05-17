using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    private float damage;
    private TowerType type;
    private float speed = 10f;
    private float aoeRadius = 1.5f;

    public void Initialize(Enemy _target, float _damage, TowerType _type)
    {
        target = _target;
        damage = _damage;
        type = _type;
    }

    void Update()
    {
        if (target == null) { Destroy(gameObject); return; }
        transform.position = Vector3.MoveTowards(
            transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            HitTarget();
    }

    void HitTarget()
    {
        if (type == TowerType.Cannon)
            AoEDamage();
        else
        {
            if (target != null) target.TakeDamage(damage);
            if (type == TowerType.Freeze && target != null)
                target.ApplyFreeze(2f, 0.5f);
        }
        Destroy(gameObject);
    }

    void AoEDamage()
    {
        Enemy[] all = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy e in all)
        {
            if (Vector3.Distance(transform.position, e.transform.position) <= aoeRadius)
                e.TakeDamage(damage);
        }
    }
}