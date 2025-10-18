using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] GameObject controlledCharacter;
    [SerializeField] GameObject watchedCharacter;

    private List<IMover> _movedCharacter;
    private List<IJumper> _jumpedCharacter;
    private List<IWatcher> _watchedCharacter;

    private CharacterControl _inputAction;
    private Vector2 _moveVector = Vector2.zero;
    private Vector2 _lookVector = Vector2.zero;
    private bool _isFirstPersonView = false;
    private bool _jump = false;

    private void Awake()
    {
        _inputAction = new CharacterControl();
        _movedCharacter = controlledCharacter.GetComponents<IMover>().ToList();
        _jumpedCharacter = controlledCharacter.GetComponents<IJumper>().ToList();
        _watchedCharacter = watchedCharacter.GetComponents<IWatcher>().ToList();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Character.Movement.performed += OnMovementPerformed;
        _inputAction.Character.Movement.canceled += OnMovementCancelled;
        _inputAction.Character.Targeting.performed += OnTargetingPerformed;
        _inputAction.Character.SwitchView.started += OnSwitchViewStarted;
        _inputAction.Character.SwitchView.canceled += OnSwitchViewCancelled;
        _inputAction.Character.Jump.started += OnJumpStarted;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Character.Movement.performed -= OnMovementPerformed;
        _inputAction.Character.Movement.canceled -= OnMovementCancelled;
        _inputAction.Character.Targeting.performed -= OnTargetingPerformed;
        _inputAction.Character.SwitchView.started -= OnSwitchViewStarted;
        _inputAction.Character.SwitchView.canceled -= OnSwitchViewCancelled;
        _inputAction.Character.Jump.started -= OnJumpStarted;
    }

    private void Update()
    {
        _movedCharacter.ForEach(item => item.Move(_moveVector));

        foreach (IWatcher watcher in _watchedCharacter)
        {
            watcher.Look(_lookVector);
            if (_isFirstPersonView)
                watcher.LookFirstPerson();
            else
                watcher.LookThirdPerson();
        }

        if (_jump)
        {
            _jumpedCharacter.ForEach(item => item.Jump());
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

    private void OnTargetingPerformed(InputAction.CallbackContext value)
    {
        _lookVector = value.ReadValue<Vector2>();
    }

    private void OnSwitchViewStarted(InputAction.CallbackContext value)
    {
        _isFirstPersonView = value.ReadValue<float>() > 0;
    }

    private void OnSwitchViewCancelled(InputAction.CallbackContext value)
    {
        _isFirstPersonView = value.ReadValue<float>() > 0;
    }

    private void OnJumpStarted(InputAction.CallbackContext value)
    {
        _jump = value.ReadValue<float>() > 0;
    }

}
