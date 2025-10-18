using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour, IItem
{
    [SerializeField] GameObject _model;
    [SerializeField] Sprite _icon;

    public Sprite ItemIcon { get => _icon; }
    public GameObject ItemModel { get => _model; }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

}
