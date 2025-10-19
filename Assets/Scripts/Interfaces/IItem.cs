using UnityEngine;
using UnityEngine.UI;

public interface IItem
{
    bool IsGun { get; }
    Sprite ItemIcon { get; }
    GameObject ItemModel { get; }
    void Use();
}

