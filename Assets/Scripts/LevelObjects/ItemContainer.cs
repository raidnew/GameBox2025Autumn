using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    static public Action<IItem> PickupItem;

    [SerializeField] GameObject itemObject;
    [SerializeField] bool immediatlyUse;
    [SerializeField] bool getByTouch = true;

    private IItem _item;

    private void Awake()
    {
        itemObject.TryGetComponent<IItem>(out _item);
    }

    private void OnTriggerEnter(Collider other)
    {
        IItemsUser itemUser;

        if(other.TryGetComponent<IItemsUser>(out itemUser) && getByTouch)
        {
            PickupItem?.Invoke(_item);
            if(immediatlyUse)
                itemUser.Get(_item);
        }
    }
}
