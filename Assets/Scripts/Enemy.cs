using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : Actor
{
    #region Variables
    [SerializeField] float minMoveSpeed = 1.0f;
    [SerializeField] float maxMoveSpeed = 5.0f;

    [SerializeField] float minFindInterval = 3.0f;
    [SerializeField] float maxFindInterval = 10.0f;

    [SerializeField] LayerMask unWalkableMask;

    protected Transform target = null;
    protected Vector3 dir = Vector3.zero;

    protected float moveSpeed = 5.0f;
    float findInterval = 5.0f;

    float startFindTime = 0.0f;
    #endregion Variables

    #region Property
    public string FilePath { get; set; }
    #endregion Property

    #region Handler
    public System.Action checkRemainEnemyHandler;
    #endregion Handler

    #region Actor Methods
    public override void InitializeActor()
    {
        base.InitializeActor();

        target = InGameSceneManager.instance.Player.transform;

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        findInterval = Random.Range(minFindInterval, maxFindInterval);
        startFindTime = -findInterval;
    }
    public override void UpdateActor()
    {
        UpdateMove();
    }

    public override void OnDead()
    {
        base.OnDead();

        checkRemainEnemyHandler?.Invoke();

        InGameSceneManager.instance.EnemyManager.Remove(FilePath, gameObject);
    }
    #endregion Actor Methods

    #region Helper Methods
    public void UpdateMove()
    {
        if (Time.time - startFindTime > findInterval)
        {
            startFindTime = Time.time;

            dir = (target.position - transform.position).normalized;
        }

        Vector3 moveVector = dir * moveSpeed * Time.deltaTime;
        if (CheckUnWalkable(moveVector))
        {
            transform.position += moveVector;
        }
    }

    bool CheckUnWalkable(Vector3 moveVector)
    {
        if (Physics.Linecast(transform.position, transform.position + moveVector, unWalkableMask))
        {
            startFindTime = Time.time;
            dir = (target.position - transform.position).normalized;

            return false;
        }

        return true;
    }

    public void SetHealth(int factor)
    {
        maxHealth += factor;
        currentHealth = maxHealth;
    }
    #endregion Helper Methods
}
