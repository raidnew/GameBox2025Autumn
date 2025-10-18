using Cinemachine;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour, IMover, IJumper
{
    [SerializeField] private Rigidbody _characterRb;
    [SerializeField] private CinemachineFreeLook _camera;

    private Vector3 _forwardVector;

    public void Jump()
    {
        _characterRb.AddForce(Vector3.up * 200);
    }

    public void Move(Vector2 direction)
    {
        //_forwardVector.IsUnifor

        _characterRb.velocity = transform.forward * direction.y + transform.right * direction.x;
        //_characterRb.velocity = new Vector3(direction.x * 5f, _characterRb.velocity.y, direction.y * 5f);
    }

    private void FixedUpdate()
    {
        _forwardVector = transform.position - _camera.transform.position;
        _forwardVector.y = 0;
        _forwardVector.Normalize();
        transform.forward = _forwardVector;
    }

}
