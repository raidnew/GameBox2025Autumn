using UnityEngine;
using UnityEngine.UI;

public interface IItem
{
    bool IsGun { get; }
    string Name { get; }
    Sprite ItemIcon { get; }
    GameObject ItemModel { get; }
    void Use();
}

