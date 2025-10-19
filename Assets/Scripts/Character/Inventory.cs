using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _itemUserObject;
    [SerializeField] private CharacterInput _input;
    private List<IItem> _items = new List<IItem>();
    private IItemsUser _itemUser;

    private void Awake()
    {
        _itemUserObject.TryGetComponent<IItemsUser>(out _itemUser);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void AddToInventory(IItem item)
    {
        _items.Add(item);
    }
    private void UseInventory()
    {
        _itemUser.Get(_items[0]);
    }
}
