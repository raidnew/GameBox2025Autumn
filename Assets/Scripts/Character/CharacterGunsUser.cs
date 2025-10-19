using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGunsUser : MonoBehaviour, IItemsUser
{
    public Action Armed;
    public Action DisArmed;

    [SerializeField] private CharacterInput _input;
    [SerializeField] private Transform _rightHandConnector;
    private IGun _currentGun;
    private GameObject _currentGunObject;

    public void Get(IItem item)
    {
        GetToRightHand(item.ItemModel);
    }

    private void HideCurrentGun()
    {
        if (_currentGunObject == null) return;
        _currentGun = null;
        Destroy(_currentGunObject );
        DisArmed?.Invoke();
    }

    private void GetToRightHand(GameObject item)
    {
        HideCurrentGun();
        _currentGunObject = Instantiate(item, _rightHandConnector, false);
        if(_currentGunObject.TryGetComponent<IGun>(out _currentGun))
        {
            _currentGun.TriggerOn();
            Armed?.Invoke();
        }
    }

    private void OnEnable()
    {
        _input.GunTriggerPress += Shoot;
        _input.GunTriggerRelease += StopShoot;
    }

    private void OnDisable()
    {
        _input.GunTriggerPress -= Shoot;
        _input.GunTriggerRelease -= StopShoot;
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
}
