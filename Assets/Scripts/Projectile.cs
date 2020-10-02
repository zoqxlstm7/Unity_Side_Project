using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float damage = 3.0f;
    [SerializeField] float rotateAngle = 10000.0f;

    Actor owner = null;
    Transform target = null;
    int targetLayer = -1;

    bool isMove = false;
    bool isReturn = false;
    #endregion Variables

    #region Property
    public string FilePath { get; set; }
    #endregion Property

    #region Unity Methods
    private void Update()
    {
        UpdateRotate();
        UpdateMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isReturn)
        {
            if(other.gameObject.layer == owner.gameObject.layer)
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
    #endregion Unity Methods

    #region Helper Methods
    void UpdateMove()
    {
        if (!isMove)
            return;

        Vector3 dir = (target.position - transform.position).normalized;

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
    #endregion Helper Methods

    #region Other Methods
    public void Fire(Actor owner, Transform target, LayerMask targetMask)
    {
        this.owner = owner;
        this.target = target;
        targetLayer = (int)Mathf.Log(targetMask.value, 2);

        transform.localScale = Vector3.one * 2f;
        isMove = true;
    }
    #endregion Other Methods
}
