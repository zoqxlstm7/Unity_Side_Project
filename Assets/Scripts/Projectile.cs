using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    [SerializeField] protected float moveSpeed = 10.0f;
    [SerializeField] protected float damage = 3.0f;

    [SerializeField] protected LayerMask removeMask;

    public Vector3 dir = Vector3.zero;

    protected Actor owner = null;
    protected Transform target = null;
    protected int targetLayer = -1;
    protected LayerMask targetMask;

    protected bool isMove = false;
    #endregion Variables

    #region Property
    public string FilePath { get; set; }
    #endregion Property

    #region Unity Methods
    private void OnEnable()
    {
        ResetProjectile();
    }
    private void Update()
    {
        UpdateProjectile();
    }

    public virtual void UpdateProjectile()
    {
    }

    public virtual void ResetProjectile()
    {
        targetLayer = -1;
        isMove = false;
    }
    #endregion Unity Methods

    #region Helper Methods
    public virtual void UpdateMove()
    {
    }

    public virtual void Remove()
    {
        InGameSceneManager.instance.ProjectileManager.Remove(FilePath, gameObject);
    }

    public virtual void CheckRemove()
    {
    }

    public void CheckTargetDead()
    {
        if (target.GetComponent<IDamageable>().IsDead)
            Remove();
    }

    public void CheckRemoveMask()
    {
        Vector3 moveVector = dir * moveSpeed * Time.deltaTime;
        if (Physics.Linecast(transform.position, transform.position + moveVector, removeMask))
            Remove();
    }
    #endregion Helper Methods

    #region Other Methods
    public virtual void Fire(Actor owner, Transform target, LayerMask targetMask)
    {
        this.owner = owner;
        this.target = target;
        this.targetMask = targetMask;
        targetLayer = (int)Mathf.Log(targetMask.value, 2);

        if(target != null)
            dir = (target.position - transform.position).normalized;

        isMove = true;
    }
    #endregion Other Methods
}
