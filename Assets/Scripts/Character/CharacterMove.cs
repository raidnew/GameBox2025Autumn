using Cinemachine;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterMove : MonoBehaviour, IMover, IJumper
{
    [SerializeField] private Rigidbody _characterRb;
    /*
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;
    [SerializeField] private CinemachineFreeLook _firstPersonCamera;
    */
    [SerializeField] private GameObject _watcherObject;

    private Vector3 _forwardVector;
    private IWatcher _watcher;

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

    private void Awake()
    {
        _watcherObject.TryGetComponent<IWatcher>(out _watcher);
    }

    private void FixedUpdate()
    {
        Vector3 moveDiretion = new Vector3(_watcher.LookDirection.x, 0, _watcher.LookDirection.z);
        moveDiretion.Normalize();
        transform.forward = moveDiretion;
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

}
