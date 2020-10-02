using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float damage = 3.0f;
    [SerializeField] float rotateAngle = 10000.0f;
    [SerializeField] float sizeScale = 1.5f;
    [SerializeField] LayerMask removeMask;

    Vector3 dir = Vector3.zero;

    Actor owner = null;
    Transform target = null;
    int targetLayer = -1;

    bool isMove = false;
    bool isReturn = false;

    bool isReturnProjectile = false;
    #endregion Variables

    #region Property
    public string FilePath { get; set; }
    #endregion Property

    #region Unity Methods
    private void Update()
    {
        CheckRemove();

        UpdateRotate();
        UpdateMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReturnProjectile)
        {
            if (isReturn)
            {
                if (other.gameObject.layer == owner.gameObject.layer)
                {
                    Remove();
                }
            }

            if (other.gameObject.layer == targetLayer)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage, null);

                transform.localScale = Vector3.one;
                target = owner.transform;
                isReturn = true;
            }
        }
        else
        {
            if (other.gameObject.layer == targetLayer)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage, null);
                Remove();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(isReturn)
        {
            if (other.gameObject.layer == owner.gameObject.layer)
            {
                Remove();
            }
        }
    }
    #endregion Unity Methods

    #region Helper Methods
    public void UpdateMove()
    {
        if (!isMove)
            return;

        if (isReturnProjectile)
            dir = (target.position - transform.position).normalized;
        else
            transform.forward = dir;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void UpdateRotate()
    {
        transform.Rotate(Vector3.up * rotateAngle * Time.deltaTime);
    }

    void Remove()
    {
        targetLayer = -1;

        isReturn = false;
        isMove = false;

        InGameSceneManager.instance.ProjectileManager.Remove(FilePath, gameObject);
    }

    void CheckRemove()
    {
        if (target.GetComponent<IDamageable>().IsDead)
            Remove();

        Vector3 moveVector = dir * moveSpeed * Time.deltaTime;
        if (Physics.Linecast(transform.position, transform.position + moveVector, removeMask))
        {
            Remove();
        }
    }
    #endregion Helper Methods

    #region Other Methods
    public void Fire(Actor owner, Transform target, LayerMask targetMask, bool isReturnProjectile = true)
    {
        this.owner = owner;
        this.target = target;
        targetLayer = (int)Mathf.Log(targetMask.value, 2);
        this.isReturnProjectile = isReturnProjectile;

        dir = (target.position - transform.position).normalized;

        if (isReturnProjectile)
            transform.localScale = Vector3.one * sizeScale;

        isMove = true;
    }
    #endregion Other Methods
}
