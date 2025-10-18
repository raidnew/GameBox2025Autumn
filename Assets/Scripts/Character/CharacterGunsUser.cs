using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGunsUser : MonoBehaviour, IItemUser
{
    [SerializeField] private Transform _rightHandConnector;

    public void Get(IItem item)
    {
        GetToRightHand(item.ItemModel);
    }

    public void GetToRightHand(GameObject item)
    {
        GameObject gun = Instantiate(item, _rightHandConnector, false);
    }


}
