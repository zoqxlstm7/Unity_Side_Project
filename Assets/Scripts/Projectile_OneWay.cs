using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_OneWay : Projectile
{
    #region Unity Methods
    public override void UpdateProjectile()
    {
        if (!isMove)
            return;

        CheckRemove();
        UpdateMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isMove)
            return;

        if (other.gameObject.layer == targetLayer)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage, null);
            Remove();
        }
    }
    #endregion Unity Methods

    #region Helper Methods
    public override void UpdateMove()
    {
        transform.forward = dir;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public override void CheckRemove()
    {
        CheckTargetDead();
        CheckRemoveMask();
    }
    #endregion Helper Methods
}
