using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{

    public Action<Vector2> Move;
    public Action<Vector2> LookMove;
    public Action Jump;
    public Action<ViewType> SwitchView;
    public Action GunTriggerPress;
    public Action GunTriggerRelease;
    public Action BeginGrenadeThrow;

    private CharacterControl _inputAction;

    private void Awake()
    {
        _inputAction = new CharacterControl();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Character.Movement.performed += OnMovementPerformed;
        _inputAction.Character.Movement.canceled += OnMovementCancelled;
        _inputAction.Character.Targeting.performed += OnTargetingPerformed;
        _inputAction.Character.SwitchView.started += OnSwitchViewAction;
        _inputAction.Character.SwitchView.canceled += OnSwitchViewAction;
        _inputAction.Character.Shoting.started += OnShootStarted;
        _inputAction.Character.Shoting.canceled += OnShootCancelled;
        _inputAction.Character.Jump.started += OnJumpStarted;
        _inputAction.Character.Grenade.started += OnGrenadeThrow;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Character.Movement.performed -= OnMovementPerformed;
        _inputAction.Character.Movement.canceled -= OnMovementCancelled;
        _inputAction.Character.Targeting.performed -= OnTargetingPerformed;
        _inputAction.Character.SwitchView.started -= OnSwitchViewAction;
        _inputAction.Character.SwitchView.canceled -= OnSwitchViewAction;
        _inputAction.Character.Shoting.started -= OnShootStarted;
        _inputAction.Character.Shoting.canceled -= OnShootCancelled;
        _inputAction.Character.Jump.started -= OnJumpStarted;
        _inputAction.Character.Grenade.started -= OnGrenadeThrow;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        Move?.Invoke(value.ReadValue<Vector2>());
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        Move?.Invoke(Vector2.zero);
    }

    private void OnTargetingPerformed(InputAction.CallbackContext value)
    {
        LookMove?.Invoke(value.ReadValue<Vector2>());
    }

    private void OnSwitchViewAction(InputAction.CallbackContext value)
    {
        SwitchView?.Invoke(value.ReadValue<float>() > 0 ? ViewType.First : ViewType.Third );
    }

    private void OnJumpStarted(InputAction.CallbackContext value)
    {
        Jump?.Invoke();
    }

    private void OnShootStarted(InputAction.CallbackContext value)
    {
        GunTriggerPress?.Invoke();
    }

    private void OnShootCancelled(InputAction.CallbackContext value)
    {
        GunTriggerRelease?.Invoke();
    }

    private void OnGrenadeThrow(InputAction.CallbackContext value)
    {
        BeginGrenadeThrow?.Invoke();
    }
}
