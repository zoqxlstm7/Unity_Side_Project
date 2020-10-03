using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : Actor
{
    #region Variables
    readonly string PROJECTILE_FILE_PATH = "Projectile/Projectile_HalfMoon";
    readonly string PROJECTILE_SPLIT_FILE_PATH = "Projectile/Projectile_Split";

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float attackRange = 5.0f;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] int projectileCount = 3;
    public int calcProjectileCount = 0;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask unWalkableMask;
    [SerializeField] Transform fireTransform = null;
    [SerializeField] float aroundDistance = 1.5f;

    Transform target = null;
    float distanceToTarget = 0.0f;

    float startAttackTime = 0.0f;

    List<Projectile> aroundProjectileList = new List<Projectile>();
    #endregion Variables

    #region Actor Methods
    public override void InitializeActor()
    {
        base.InitializeActor();

        calcProjectileCount = projectileCount;
    }
    public override void UpdateActor()
    {
        if (IsDead)
            return;

        UpdateMove();
        SearchEnemy();

        CheckAttaack();
        AroundProjectileRotate();

        InputSplit();
    }

    void InputSplit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < projectileCount; i++)
            {
                if (projectileCount != aroundProjectileList.Count)
                    return;

                Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate(PROJECTILE_SPLIT_FILE_PATH, fireTransform.position);
                projectile.Fire(this, aroundProjectileList[i].transform, targetMask);
            }
        }
    }

    public override void OnDead()
    {
        base.OnDead();

        InGameSceneManager.instance.isGameOver = true;
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
            if (calcProjectileCount <= 0)
            {
                calcProjectileCount = projectileCount;
                startAttackTime = Time.time;

                if(aroundProjectileList.Count > 0)
                    StartCoroutine(LaunchAroundProjectile());
                return;
            }

            calcProjectileCount--;

            Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate(PROJECTILE_FILE_PATH, fireTransform.position);
            projectile.Fire(this, target, targetMask);

            startAttackTime = Time.time;
        }
    }    

    IEnumerator LaunchAroundProjectile()
    {
        float intervalTime = 0.2f;
        float launchTime = Time.time;

        while (aroundProjectileList.Count > 0)
        {
            if (target == null)
                break;

            if(Time.time - launchTime > intervalTime)
            {
                aroundProjectileList[0].Fire(this, target, targetMask);
                aroundProjectileList.RemoveAt(0);
                launchTime = Time.time;
            }

            yield return null;
        }

        yield return new WaitForSeconds(intervalTime);
        SetProjectile();
    }

    public void SetProjectile()
    {
        for (int i = 0; i < aroundProjectileList.Count; i++)
        {
            InGameSceneManager.instance.ProjectileManager.Remove(aroundProjectileList[i].FilePath, aroundProjectileList[i].gameObject);
        }
        aroundProjectileList.Clear();

        Vector3 startPos = fireTransform.position;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * (360.0f / projectileCount);
            Vector3 dir = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;
            Vector3 position = startPos + dir * aroundDistance;

            Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate(PROJECTILE_FILE_PATH, position);
            aroundProjectileList.Add(projectile);
        }
    }

    void AroundProjectileRotate()
    {
        for (int i = 0; i < aroundProjectileList.Count; i++)
        {
            Vector3 startPos = fireTransform.position;

            float angle = i * (360.0f / aroundProjectileList.Count);
            Vector3 dir = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;
            Vector3 position = startPos + dir * aroundDistance;
            aroundProjectileList[i].transform.position = position;

            //aroundProjectileList[i].transform.RotateAround(transform.position, Vector3.up, 100.0f * Time.deltaTime);
            //aroundProjectileList[i].transform.LookAt(transform);
        }
    }
    #endregion Helper Methods
}
