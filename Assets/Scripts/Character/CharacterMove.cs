using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Action<Vector2> Move;
    public Action LeaveGround;
    public Action GetLanded;

    [SerializeField] private CharacterInput _input;
    [SerializeField] private Rigidbody _characterRb;
    [SerializeField] private GameObject _watcherObject;
    [SerializeField] private float _jumpPower = 250;
    [SerializeField] private CharacterActions _characterAction;

    private bool _isMoving;
    private Vector3 _moveVector;
    private Vector3 MoveVector
    {
        get => _moveVector;
        set
        {
            if(value != _moveVector)
            {
                _moveVector = value;
                Move?.Invoke(_moveVector);
                _isMoving = _moveVector.magnitude > 0;
            }
        }
    }

    private IWatcher _watcher;

    private void Awake()
    {
        _watcherObject.TryGetComponent<IWatcher>(out _watcher);
    }

    private void OnEnable()
    {
        _input.Move += OnMove;
        _input.Jump += OnJump;
    }

    private void OnDisable()
    {
        _input.Move -= OnMove;
        _input.Jump -= OnJump;
    }

    private void Start()
    {
        MoveVector = Vector2.zero;
        Move?.Invoke(_moveVector);
    }

    private void FixedUpdate()
    {
        Vector3 moveDiretion = new Vector3(_watcher.LookDirection.x, 0, _watcher.LookDirection.z);
        moveDiretion.Normalize();
        if (moveDiretion.magnitude > 0)
            _watcherObject.transform.forward = moveDiretion;
        if (_isMoving) MoveObject();
    }

    private void OnJump()
    {
        if(_characterAction.IsOnGround)
            _characterRb.AddForce(Vector3.up * _jumpPower);
    }

    private void OnMove(Vector2 direction)
    {
        MoveVector = direction;
    }

    private void MoveObject()
    {
        Vector3 velocityVector = _watcherObject.transform.forward * MoveVector.y + _watcherObject.transform.right * MoveVector.x;
        if (!_characterAction.IsOnGround) velocityVector = velocityVector * 0.25f; //Low speed in air
        Vector3 currentVelocity = _characterRb.velocity;
        if (Math.Abs(currentVelocity.x) > Math.Abs(velocityVector.x) && velocityVector.z != 0) velocityVector.x = currentVelocity.x;
        if (Math.Abs(currentVelocity.z) > Math.Abs(velocityVector.z) && velocityVector.z != 0) velocityVector.z = currentVelocity.z;
        _characterRb.velocity = new Vector3(velocityVector.x, _characterRb.velocity.y, velocityVector.z);
    }
}
