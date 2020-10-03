using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Projectile : Item
{
    private void OnTriggerEnter(Collider other)
    {
        int layerIndex = (int)Mathf.Log(targetMask, 2);
        if(other.gameObject.layer == layerIndex)
        {
            AddedItemValue();
        }
    }

    public override void AddedItemValue()
    {
        InGameSceneManager.instance.Player.AddedProjectile((int)value);
        Destroy(gameObject);
    }
}
