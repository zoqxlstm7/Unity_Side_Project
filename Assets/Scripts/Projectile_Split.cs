using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Split : Projectile
{
    #region Variables
    [SerializeField] float rotateAngle = 10000.0f;
    [SerializeField] float initialSizeScale = 2.0f;
    #endregion Variables

    #region Unity Methods
    public override void ResetProjectile()
    {
        base.ResetProjectile();

        transform.localScale = Vector3.one * initialSizeScale;
    }

    public override void UpdateProjectile()
    {
        CheckSplitMask();

        UpdateRotate();
        UpdateMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage, null);
        }
    }
    #endregion Unity Methods

    #region Helper Methods
    public override void UpdateMove()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void UpdateRotate()
    {
        transform.Rotate(Vector3.up * rotateAngle * Time.deltaTime);
    }
    #endregion Helper Methods

    #region Other Methods
    void CheckSplitMask()
    {
        if (transform.localScale.x <= 0.75f)
        {
            Remove();
            return;
        }

        int count = 3;
        RaycastHit hit;
        if(Physics.Linecast(transform.position, transform.position + dir, out hit, removeMask))
        {
            for (int i = 0; i < count; i++)
            {
                Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate("Projectile/Projectile_Split", transform.position + hit.normal);
                projectile.transform.localScale = transform.localScale * 0.75f;
                projectile.Fire(owner, null, targetMask);
                projectile.dir = Quaternion.Euler(0.0f, (180 / count) * (i + 1), 0.0f) * hit.normal; 
            }

            Remove();
            //Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate("Projectile/Projectile_Split", transform.position + hit.normal);
            //projectile.transform.localScale = transform.localScale * 0.75f;
            //Remove();
            //dir = Quaternion.Euler(0.0f, Random.Range(0, 360), 0.0f) * hit.normal;
            //transform.localScale = transform.localScale * 0.75f;

            //if (transform.localScale.x <= 0.5f)
            //    Remove();
        }
    }
    #endregion Other Methods
}
