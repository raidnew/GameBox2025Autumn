using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IItemView

{
    [SerializeField] private Image _select;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;

    public void Init(IItem item)
    {
        _select.enabled = false;
        _icon.sprite = item.ItemIcon;
        _text.text = item.Name;
    }

    public void Select(bool enable)
    {
        _select.enabled = enable;
    }
}
