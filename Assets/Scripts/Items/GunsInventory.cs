using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryOwnerObject;
    private IItemsUser _inventoryOwner;

    private GunType[] _orderGuns = { GunType.ShotGun, GunType.Automatic };
    private IItem[] _gunsInventory;

    private void Awake()
    {
        _gunsInventory = new IItem[_orderGuns.Length];
        _inventoryOwnerObject.TryGetComponent<IItemsUser>(out _inventoryOwner);
    }

    private void OnEnable()
    {
        ItemContainer.PickupItem += OnPickupItem;
    }

    private void OnDisable()
    {
        ItemContainer.PickupItem -= OnPickupItem;
    }

    private void OnPickupItem(IItem item)
    {
        IGun gun;
        if(item.IsGun && item.ItemModel.TryGetComponent<IGun>(out gun))
        {
            int index = Array.FindIndex(_orderGuns, guntype => guntype == gun.GetGunType());
            _gunsInventory[index] = item;
        }
    }
}
