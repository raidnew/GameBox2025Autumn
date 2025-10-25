using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverntoryView : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryObject;
    [SerializeField] private GameObject _itemViewPrefab;
    [SerializeField] private GameObject _itemsContainer;

    private IInventory _inventory;
    private bool _hasSelectedItem;
    private InventoryItem _currentSelected;
    private List<ItemInInventory> _items = new List<ItemInInventory>();

    private void Awake()
    {
        _inventoryObject.TryGetComponent<IInventory>(out _inventory);
    }

    private void OnEnable()
    {
        _inventory.ItemAdded += OnItemAdded;
        _inventory.ItemRemoved += OnItemRemoved;
        _inventory.ItemSelected += OnItemSelected;
    }

    private void OnDisable()
    {
        _inventory.ItemAdded -= OnItemAdded;
        _inventory.ItemRemoved -= OnItemRemoved;
        _inventory.ItemSelected -= OnItemSelected;
    }

    private void OnItemSelected(IItem selectedItem)
    {
        if (_hasSelectedItem) _currentSelected.Select(false);
        ItemInInventory inventoryItem = _items.Find(item => item.item == selectedItem);
        inventoryItem.itemView.Select(true);
        _currentSelected = inventoryItem.itemView;
        _hasSelectedItem = true;
    }

    private void OnItemRemoved(IItem item)
    {
        
    }

    private void OnItemAdded(IItem item)
    {

        int orderInInventory = 0;
        if (item.IsGun && item.ItemModel.TryGetComponent<IGun>(out IGun gun))
            orderInInventory = (int)gun.GetGunType();
        _items.Add(CreateInventoryCell(item, new Vector3(orderInInventory * 80, 0, 0)));
    }

    private ItemInInventory CreateInventoryCell(IItem item, Vector3 position)
    {
        GameObject view = Instantiate(_itemViewPrefab, _itemsContainer.transform, true);
        RectTransform rt = view.GetComponent<RectTransform>();
        InventoryItem itemView = view.GetComponent<InventoryItem>();
        itemView.Init(item);
        itemView.Select(false);
        ItemInInventory inventory = new ItemInInventory { item = item, itemView = itemView };
        rt.localPosition = position;
        return inventory;
    }

    private struct ItemInInventory
    {
        public InventoryItem itemView;
        public IItem item;
    }
}
