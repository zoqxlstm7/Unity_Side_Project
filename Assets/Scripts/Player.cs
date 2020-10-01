using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    #region Variables
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float attackRange = 5.0f;
    [SerializeField] float attackSpeed = 0.5f;

    [SerializeField] LayerMask targetMask;
    [SerializeField] Transform fireTransform = null;

    Transform target = null;
    float distanceToTarget = 0.0f;

    float startAttackTime = 0.0f;
    #endregion Variables

    #region Actor Methods
    public override void UpdateActor()
    {
        UpdateMove();
        SearchEnemy();

        CheckAttaack();
    }
    #endregion Actor Methods

    #region Helper Methods
    void UpdateMove()
    {
        Vector2 inputVector = InGameSceneManager.instance.JoyStick.GetInputVector();
        if (inputVector == Vector2.zero)
            return;

        Vector3 moveVector = new Vector3(inputVector.x, 0.0f, inputVector.y);

        transform.forward = moveVector;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    void SearchEnemy()
    {
        target = null;
        distanceToTarget = 0.0f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        if(colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<IDamageable>().IsDead)
                    continue;

                Transform findTarget = colliders[i].transform;

                float distanceToFindTarget = Vector3.Distance(transform.position, findTarget.position);
                if (target == null || distanceToTarget > distanceToFindTarget)
                {
                    target = findTarget;
                    distanceToTarget = distanceToFindTarget;
                }
            }
        }
    }

    void CheckAttaack()
    {
        if (target == null)
            return;

        if(Time.time - startAttackTime > attackSpeed)
        {
            string filePath = "Projectile/Projectile";
            Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate(filePath, fireTransform.position);
            projectile.Fire(this, target, targetMask);

            startAttackTime = Time.time;
        }
    }
    #endregion Helper Methods
}
