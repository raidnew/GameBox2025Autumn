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
            _currentGun.Shot();
            Armed?.Invoke();
        }
    }

    private void OnEnable()
    {
        _input.GunTriggerPress += Shoot;
    }

    private void OnDisable()
    {
        _input.GunTriggerPress -= Shoot;
    }

    private void Shoot()
    {
        if (_currentGun == null) return;
        _currentGun.Shot();

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        RaycastHit gunhit;

        if (Physics.Raycast(ray, out hit))
        {
            Ray shootRay = new Ray(_currentGun.Muzzle, hit.point - _currentGun.Muzzle);
            if(Physics.Raycast(shootRay, out gunhit))
            {
                Debug.DrawLine(_currentGun.Muzzle, gunhit.point, Color.red, 2f);
            }
        }

        
    }
}
