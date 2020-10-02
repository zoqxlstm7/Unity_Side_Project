using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : Actor, IAttackable
{
    #region Variables
    [SerializeField] float minMoveSpeed = 1.0f;
    [SerializeField] float maxMoveSpeed = 5.0f;

    [SerializeField] float minFindInterval = 3.0f;
    [SerializeField] float maxFindInterval = 10.0f;

    [SerializeField] LayerMask unWalkableMask;

    Transform target = null;
    Vector3 dir = Vector3.zero;

    float moveSpeed = 5.0f;
    float findInterval = 5.0f;

    float startFindTime = 0.0f;
    #endregion Variables

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
    #endregion Actor Methods

    #region Helper Methods
    void UpdateMove()
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
    #endregion Helper Methods

    #region IAttackable Interface
    public AttackBehaviour CurrentAttackBehaviour { get; set; }

    public void OnExecuteAttack(int animationIndex)
    {
    }
    #endregion IAttackable Interface
}
