using UnityEngine;
using UnityEngine.UI;

public interface IItem
{
    Sprite ItemIcon { get; }
    GameObject ItemModel { get; }

    void Use();
}

