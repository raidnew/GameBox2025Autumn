using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGunsUser : MonoBehaviour, IItemsUser
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

    public void Shoot()
    {

    }


}
