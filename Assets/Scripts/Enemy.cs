using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor, IAttackable
{
    #region Variables
    [SerializeField] float moveSpeed = 5.0f;
    #endregion Variables

    #region IAttackable Interface
    public AttackBehaviour CurrentAttackBehaviour { get; set; }

    public void OnExecuteAttack(int animationIndex)
    {
    }
    #endregion IAttackable Interface

    
}
