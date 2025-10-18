using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour, IMover, IJumper
{
    [SerializeField] private Rigidbody _characterRb;
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;
    [SerializeField] private CinemachineFreeLook _firstPersonCamera;

    private Vector3 _forwardVector;

    private List<IGround> _grounds = new List<IGround>();
    private bool IsOnGround { get => _grounds.Count > 0; }
    public Action LeaveGround { get; set; }
    public Action GetLanded { get; set; }

    public void Jump()
    {
        _characterRb.AddForce(Vector3.up * 200);
    }

    public void Move(Vector2 direction)
    {
        Vector3 velocityVector = transform.forward * direction.y + transform.right * direction.x;
        if (!IsOnGround) velocityVector = velocityVector * 0.025f; //Low speed in air
        _characterRb.velocity = new Vector3(velocityVector.x, _characterRb.velocity.y, velocityVector.z);
    }

    private void FixedUpdate()
    {
        CalcForward();
    }

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

    private void CalcForward()
    {
        _forwardVector = transform.position - _thirdPersonCamera.transform.position;
        _forwardVector.y = 0;
        _forwardVector.Normalize();
        transform.forward = _forwardVector;
    }

}
