using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : Actor
{
    #region Variables
    readonly string PROJECTILE_FILE_PATH = "Projectile/Projectile_HalfMoon";

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float attackRange = 5.0f;
    [SerializeField] float attackSpeed = 0.5f;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask unWalkableMask;
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
        InputMove();

        Vector2 inputVector = InGameSceneManager.instance.JoyStick.GetInputVector();
        if (inputVector == Vector2.zero)
            return;

        Vector3 moveVector = new Vector3(inputVector.x, 0.0f, inputVector.y);
        moveVector = moveVector * moveSpeed * Time.deltaTime;
        if (CheckUnWalkable(moveVector))
        {
            transform.forward = moveVector;
            transform.position += moveVector;
        }
    }

    void InputMove()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 moveVector = new Vector3(h, 0.0f, v);
        if (moveVector != Vector3.zero)
        {
            moveVector = moveVector * moveSpeed * Time.deltaTime;
            if (CheckUnWalkable(moveVector))
            {
                transform.forward = moveVector;
                transform.position += moveVector;
            }
        }
    }

    bool CheckUnWalkable(Vector3 moveVector)
    {
        if(Physics.Linecast(transform.position, transform.position + moveVector, unWalkableMask))
        {
            return false;
        }

        return true;
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
            Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate(PROJECTILE_FILE_PATH, fireTransform.position);
            projectile.Fire(this, target, targetMask);

            startAttackTime = Time.time;
        }
    }
    #endregion Helper Methods
}
