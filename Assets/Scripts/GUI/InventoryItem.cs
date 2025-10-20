using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IItemView

{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;

    private IItem _item;
 
    public void Init(IItem item)
    {
        _icon.sprite = item.ItemIcon;
        _text.text = item.Name;
    }
}
