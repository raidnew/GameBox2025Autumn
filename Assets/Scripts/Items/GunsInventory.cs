using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsInventory : MonoBehaviour
{
    [SerializeField] private InventoryInput _input;
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
        _input.SelectGunItem += OnSelectGun;
    }

    private void OnDisable()
    {
        ItemContainer.PickupItem -= OnPickupItem;
        _input.SelectGunItem -= OnSelectGun;
    }

    private void OnPickupItem(IItem item)
    {
        IGun gun;
        if(item.IsGun && item.ItemModel.TryGetComponent<IGun>(out gun))
        {
            _gunsInventory[GetIndexGun(gun.GetGunType())] = item;
        }
    }

    private void OnSelectGun(GunType gunType)
    {
        if(_gunsInventory[GetIndexGun(gunType)] != null)
        {
            _inventoryOwner.Get(_gunsInventory[GetIndexGun(gunType)]);
        }
    }

    private int GetIndexGun(GunType gunType)
    {
        return Array.FindIndex(_orderGuns, type => type == gunType);
    }
}
