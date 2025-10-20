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

    private void OnItemSelected(IItem item)
    {
        
    }

    private void OnItemRemoved(IItem item)
    {
        
    }

    private void OnItemAdded(IItem item)
    {
        GameObject view = Instantiate(_itemViewPrefab);

        view.transform.position = new Vector3(_items.Count * 80, 0, 0);

        InventoryItem itemView = view.GetComponent<InventoryItem>();
        itemView.Init(item);
        ItemInInventory inventory = new ItemInInventory { item = item, itemView = itemView };
        itemView.transform.parent = _itemsContainer.transform;

        _items.Add(inventory);
    }

    private struct ItemInInventory
    {
        public InventoryItem itemView;
        public IItem item;
    }
}
