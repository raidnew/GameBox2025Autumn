using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsInventory : MonoBehaviour, IInventory
{
    public Action<IItem> ItemAdded { get; set; }
    public Action<IItem> ItemRemoved { get; set; }
    public Action<IItem> ItemSelected { get; set; }

    [SerializeField] private InventoryInput _input;

    private GunType[] _orderGuns = { GunType.ShotGun, GunType.Automatic };
    private IItem[] _gunsInventory;

    private void Awake()
    {
        _gunsInventory = new IItem[_orderGuns.Length];
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

    private void OnPickupItem(IItem item, bool immediatlyUse)
    {
        IGun gun;
        if(item.IsGun && item.ItemModel.TryGetComponent<IGun>(out gun))
        {
            if (_gunsInventory[GetIndexGun(gun.GetGunType())] != null)
            {
                Debug.Log("Gun already picked");
                return;
            }

            _gunsInventory[GetIndexGun(gun.GetGunType())] = item;
            ItemAdded?.Invoke(item);
            if (immediatlyUse) OnSelectGun(gun.GetGunType());
        }
    }

    private void OnSelectGun(GunType gunType)
    {
        if (_gunsInventory[GetIndexGun(gunType)] != null)
            ItemSelected?.Invoke(_gunsInventory[GetIndexGun(gunType)]);
    }

    private int GetIndexGun(GunType gunType)
    {
        return Array.FindIndex(_orderGuns, type => type == gunType);
    }
}
