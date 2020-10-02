using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : Enemy
{
    #region Variables
    readonly string PROJECTILE_FILE_PATH = "Projectile/Projectile_Spike";

    [SerializeField] float attackSpeed = 1.0f;

    [SerializeField] Transform fireTransform = null;
    [SerializeField] LayerMask targetMask;

    float startAttackTime = 0.0f;
    #endregion Variables

    public override void UpdateActor()
    {
        base.UpdateActor();

        CheckAttack();
    }

    void CheckAttack()
    {
        if (Time.time - startAttackTime > attackSpeed)
        {
            Projectile projectile = InGameSceneManager.instance.ProjectileManager.Generate(PROJECTILE_FILE_PATH, fireTransform.position);
            projectile.Fire(this, target, targetMask, false);

            startAttackTime = Time.time;
        }
    }
}
