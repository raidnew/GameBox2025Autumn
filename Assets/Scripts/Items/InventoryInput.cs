using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInput : MonoBehaviour
{

    public Action<GunType> SelectGunItem;
    public Action NextItem;

    private CharacterControl _inputAction;

    private void Awake()
    {
        _inputAction = new CharacterControl();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Inventory.ShotGun.started += OnSelectShotgun;
        _inputAction.Inventory.AutomaticGun.started += OnSelectAutomatic;
        _inputAction.Inventory.NextItem.started += OnSelectNextItem;
    }

    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Inventory.ShotGun.started -= OnSelectShotgun;
        _inputAction.Inventory.AutomaticGun.started -= OnSelectAutomatic;
        _inputAction.Inventory.NextItem.started -= OnSelectNextItem;
    }

    private void OnSelectNextItem(InputAction.CallbackContext context)
    {
        NextItem?.Invoke();
    }

    private void OnSelectAutomatic(InputAction.CallbackContext context)
    {
        SelectGunItem?.Invoke(GunType.Automatic);
    }

    private void OnSelectShotgun(InputAction.CallbackContext context)
    {
        SelectGunItem?.Invoke(GunType.ShotGun);
    }

}
