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

    private List<IGround> _grounds = new List<IGround>();
    private bool IsOnGround { get => _grounds.Count > 0; }

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
        Debug.DrawRay(transform.position, moveDiretion, Color.green, 0.2f);
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

    private void OnJump()
    {
        _characterRb.AddForce(Vector3.up * _jumpPower);
    }

    private void OnMove(Vector2 direction)
    {
        MoveVector = direction;
    }

    private void MoveObject()
    {
        Vector3 velocityVector = _watcherObject.transform.forward * MoveVector.y + _watcherObject.transform.right * MoveVector.x;
        if (!IsOnGround) velocityVector = velocityVector * 0.25f; //Low speed in air
        _characterRb.velocity = new Vector3(velocityVector.x, _characterRb.velocity.y, velocityVector.z);
    }
}
