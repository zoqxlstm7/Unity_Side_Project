using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable
{
    #region Variables
    [SerializeField] protected float maxHealth = 100.0f;
    [SerializeField] protected float currentHealth = 0.0f;
    #endregion Variables

    #region Unity Methods
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        InitializeActor();
    }

    private void Update()
    {
        UpdateActor();
    }
    #endregion Unity Methods

    #region Other Methods
    public virtual void InitializeActor()
    {
        
    }

    public virtual void UpdateActor()
    {

    }

    public virtual void OnDead() { }
    #endregion Other Methods

    #region IDamageable Interface
    public bool IsDead => currentHealth <= 0;

    public virtual void TakeDamage(float damage, GameObject hitEffectPrefab, Transform target = null)
    {
        if (IsDead)
            return;

        currentHealth -= damage;

        if (hitEffectPrefab)
        {
            Vector3 offset = target.position;
            offset.y += 0.5f;
            Instantiate(hitEffectPrefab, offset, Quaternion.identity);
        }

        if(IsDead)
        {
            currentHealth = 0.0f;

            OnDead();
        }
    }
    #endregion IDamageable Interface
}
