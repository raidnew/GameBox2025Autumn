using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject controlledCharacter;

    private List<IMover> _movedCharacter;
    private List<IJumper> _jumpedCharacter;

    private CharacterControl _inputAction;
    private Vector2 _moveVector = Vector2.zero;
    private bool _jump = false;

    private void Awake()
    {
        _inputAction = new CharacterControl();
        _movedCharacter = controlledCharacter.GetComponents<IMover>().ToList();
        _jumpedCharacter = controlledCharacter.GetComponents<IJumper>().ToList();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Character.Movement.performed += OnMovementPerformed;
        _inputAction.Character.Movement.canceled += OnMovementCancelled;

        _inputAction.Character.Jump.started += OnJumpStarted;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Character.Movement.performed -= OnMovementPerformed;
        _inputAction.Character.Movement.canceled -= OnMovementCancelled;

        _inputAction.Character.Jump.started -= OnJumpStarted;
    }

    private void Update()
    {
        foreach (IMover item in _movedCharacter)
            item.Move(_moveVector);

        if (_jump)
        {
            foreach (IJumper item in _jumpedCharacter)
                item.Jump();
            _jump = false;
        }
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        _moveVector = Vector2.zero;
    }

    private void OnJumpStarted(InputAction.CallbackContext value)
    {
        _jump = value.ReadValue<float>() > 0;
    }
}
