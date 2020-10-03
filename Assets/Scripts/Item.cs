using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Variables
    [SerializeField] protected float value = 0.0f;
    [SerializeField] protected LayerMask targetMask;
    #endregion Variables

    #region Other Methods
    public virtual void AddedItemValue()
    {

    }
    #endregion Other Methods
}
