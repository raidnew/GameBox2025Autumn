using UnityEngine;

public class ItemObject : MonoBehaviour, IItem
{
    [SerializeField] private bool _isGun;
    [SerializeField] private GameObject _model;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    public string Name => _name;
    public bool IsGun => _isGun; 
    public Sprite ItemIcon => _icon;
    public GameObject ItemModel => _model;

    public void Use() {}

}
