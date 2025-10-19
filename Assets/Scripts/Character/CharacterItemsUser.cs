using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemsUser : MonoBehaviour, IItemsUser
{
    [SerializeField] private Inventory _inventory;

    public void Get(IItem item)
    {
        if (!item.IsGun)
        {
            _inventory.AddToInventory(item);
        }
    }
}
