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
        currentHealth = maxHealth;
    }

    public virtual void UpdateActor()
    {

    }

    public virtual void OnDead() { }
    #endregion Other Methods

    #region IDamageable Interface
    public bool IsDead => currentHealth <= 0;

    public void TakeDamage(float damage, string hitEffectPath)
    {
        if (IsDead)
            return;

        currentHealth -= damage;

        if (!string.IsNullOrEmpty(hitEffectPath))
        {
            // 히트 이펙트 출력
        }

        if(IsDead)
        {
            currentHealth = 0.0f;

            OnDead();
        }
    }
    #endregion IDamageable Interface
}
