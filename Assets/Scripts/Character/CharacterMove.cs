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
        Vector3 velocityVector = transform.forward * direction.y + transform.right * direction.x;
        _characterRb.velocity = new Vector3(velocityVector.x, _characterRb.velocity.y, velocityVector.z);
    }

    private void FixedUpdate()
    {
        CalcForward();
    }

    private void CalcForward()
    {
        _forwardVector = transform.position - _camera.transform.position;
        _forwardVector.y = 0;
        _forwardVector.Normalize();
        transform.forward = _forwardVector;
    }

}
