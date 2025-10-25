using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    private List<IGround> _grounds = new List<IGround>();
    public bool IsOnGround { get => _grounds.Count > 0; }
    public Action LeaveGround { get; set; }
    public Action GetLanded { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        IGround ground;
        if (collision.collider.TryGetComponent<IGround>(out ground))
        {
            _grounds.Add(ground);
            if (IsOnGround) GetLanded?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IGround ground;
        if (collision.collider.TryGetComponent<IGround>(out ground))
        {
            _grounds.Remove(ground);
            if (!IsOnGround) LeaveGround?.Invoke();
        }
    }
}
