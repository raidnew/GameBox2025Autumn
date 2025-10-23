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
    private int _selectedIndex = -1;

    private void Awake()
    {
        _gunsInventory = new IItem[_orderGuns.Length];
    }

    private void OnEnable()
    {
        ItemContainer.PickupItem += OnPickupItem;
        _input.SelectGunItem += OnSelectGun;
        _input.NextItem += OnNextGun;
    }

    private void OnDisable()
    {
        ItemContainer.PickupItem -= OnPickupItem;
        _input.NextItem -= OnNextGun;
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
        SelectGun(GetIndexGun(gunType));
    }

    private int GetIndexGun(GunType gunType)
    {
        return Array.FindIndex(_orderGuns, type => type == gunType);
    }

    private void OnNextGun()
    {
        int newIndex = (_selectedIndex + 1) % _gunsInventory.Length;
        SelectGun(newIndex);
    }

    private void SelectGun(int index)
    {
        if (_gunsInventory[index] == null) return;
        _selectedIndex = index;
        ItemSelected?.Invoke(_gunsInventory[index]);
    }
}
