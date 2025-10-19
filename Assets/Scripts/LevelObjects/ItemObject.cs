using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour, IItem
{
    [SerializeField] private bool _isGun;
    [SerializeField] private GameObject _model;
    [SerializeField] private Sprite _icon;

    public bool IsGun { get => _isGun; }
    public Sprite ItemIcon { get => _icon; }
    public GameObject ItemModel { get => _model; }

    public void Use()
    {
    }

}
