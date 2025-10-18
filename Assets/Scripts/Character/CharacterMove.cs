using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour, IMover, IJumper
{
    [SerializeField] private Rigidbody _characterRb;

    public void Jump()
    {
        _characterRb.AddForce(Vector3.up * 200);
    }

    public void Move(Vector2 direction)
    {
        _characterRb.velocity = new Vector3(direction.x * 5f, _characterRb.velocity.y, direction.y * 5f);
    }

}
