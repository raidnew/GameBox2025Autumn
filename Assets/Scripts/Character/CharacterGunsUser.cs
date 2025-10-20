using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGunsUser : MonoBehaviour, IItemsUser
{
    public Action Armed;
    public Action DisArmed;
    public Action BeginGrenadeThrow;

    [SerializeField] private CharacterInput _input;
    [SerializeField] private CharacterAnimation _animation;
    [SerializeField] private Transform _rightHandConnector;
    [SerializeField] private GrenadeLauncher _grenadeLauncher;
    [SerializeField] private GunsInventory _gunInventory;

    private IGun _currentGun;
    private GameObject _currentGunObject;
    private GameObject _currentItem;

    public void Get(IItem item)
    {
        if (item.IsGun)
        {
            HideCurrentGun();
            GetToRightHand(item.ItemModel);
        }
    }

    private void OnEnable()
    {
        _input.GunTriggerPress += Shoot;
        _input.GunTriggerRelease += StopShoot;
        _input.BeginGrenadeThrow += OnBeginGrenadeThrow;
        _animation.GrenadeIsThrowing += GrenadeThrow;
        _animation.GrenadeHasThrowed += OnEndGrenadeThrow;
        _gunInventory.ItemSelected += Get;
    }

    private void OnDisable()
    {
        _input.GunTriggerPress -= Shoot;
        _input.GunTriggerRelease -= StopShoot;
        _input.BeginGrenadeThrow -= OnBeginGrenadeThrow;
        _animation.GrenadeIsThrowing -= GrenadeThrow;
        _animation.GrenadeHasThrowed -= OnEndGrenadeThrow;
        _gunInventory.ItemSelected -= Get;
    }

    private void HideCurrentGun()
    {
        if (_currentGunObject == null) return;
        _currentGun = null;
        Destroy(_currentGunObject);
        DisArmed?.Invoke();
    }

    private void GetToRightHand(GameObject item)
    {
        if (item == null) return;
        _currentItem = item;
        _currentGunObject = Instantiate(item, _rightHandConnector, false);
        if (_currentGunObject.TryGetComponent<IGun>(out _currentGun))
            Armed?.Invoke();
    }

    private void Shoot()
    {
        if (_currentGun == null) return;
        _currentGun.TriggerOn();
    }

    private void StopShoot()
    {
        if (_currentGun == null) return;
        _currentGun.TriggerOff();
    }

    private void OnBeginGrenadeThrow()
    {
        HideCurrentGun();
        BeginGrenadeThrow?.Invoke();
    }

    private void GrenadeThrow()
    {
        _grenadeLauncher.Launch();
    }

    private void OnEndGrenadeThrow()
    {
        GetToRightHand(_currentItem);
    }
}
