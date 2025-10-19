using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGunsUser : MonoBehaviour, IItemsUser
{
    public Action Armed;
    public Action DisArmed;

    public static Action<RaycastHit> BulletHit;

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
        RaycastHit viewhit;
        RaycastHit shothit;
        if (Physics.Raycast(ray, out viewhit))
        {
            Ray shootRay = new Ray(_currentGun.Muzzle, viewhit.point - _currentGun.Muzzle);
            if(Physics.Raycast(shootRay, out shothit))
            {
                //Debug.DrawLine(_currentGun.Muzzle, shothit.point, Color.red, 2f);
                BulletHit?.Invoke(shothit);
            }
        }

        
    }
}
