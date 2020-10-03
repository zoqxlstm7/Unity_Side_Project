using UnityEngine;

public interface IDamageable
{
    bool IsDead { get; }

    void TakeDamage(float damage, GameObject hitEffectPrefab, Transform target = null);
}
