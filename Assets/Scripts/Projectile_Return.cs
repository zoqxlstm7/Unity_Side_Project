using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Return : Projectile
{
    #region Variables
    [SerializeField] float rotateAngle = 10000.0f;
    [SerializeField] float sizeScale = 1.5f;
    [SerializeField] GameObject hitEffectPrefab = null;

    bool isReturn = false;
    #endregion Variables

    #region Unity Methods
    public override void ResetProjectile()
    {
        base.ResetProjectile();

        isReturn = false;
        transform.localScale = Vector3.one;
    }

    public override void UpdateProjectile()
    {
        UpdateRotate();

        if (!isMove)
            return;

        CheckRemove();

        UpdateMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isMove)
            return;

        if (isReturn)
        {
            if (other.gameObject.layer == owner.gameObject.layer)
            {
                Remove();
            }
        }
        else
        {
            if (other.gameObject.layer == targetLayer)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage, hitEffectPrefab, other.transform);

                transform.localScale = Vector3.one;
                target = owner.transform;
                isReturn = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isMove)
            return;

        if (!isReturn)
            return;

        if (other.gameObject.layer == owner.gameObject.layer)
            Remove();
    }
    #endregion Unity Methods

    #region Helper Methods
    public override void UpdateMove()
    {
        dir = (target.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void UpdateRotate()
    {
        transform.Rotate(Vector3.up * rotateAngle * Time.deltaTime);
    }

    public override void CheckRemove()
    {
        CheckTargetDead();
    }
    #endregion Helper Methods

    #region Other Methods
    public override void Fire(Actor owner, Transform target, LayerMask targetMask)
    {
        base.Fire(owner, target, targetMask);

        transform.localScale = Vector3.one * sizeScale;
    }
    #endregion Other Methods
}
