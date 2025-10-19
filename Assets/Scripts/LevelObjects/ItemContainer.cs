using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] GameObject itemObject;

    private IItem item;

    private void Awake()
    {
        itemObject.TryGetComponent<IItem>(out item);
    }

    private void OnTriggerEnter(Collider other)
    {
        IItemsUser itemUser;
        if(other.TryGetComponent<IItemsUser>(out itemUser))
        {
            itemUser.Get(item);
        }
    }
}
