using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutofireGun : FirearmsGun
{
    [SerializeField] private float _delayBeetweenShots;

    private float _shotTimer = 0;
    private bool isActive;

    public override void TriggerOn()
    {
        isActive = true;
    }

    public override void TriggerOff()
    {
        isActive = false;
    }

    private void Update()
    {
        if(isActive && _shotTimer <= 0)
        {
            Hammer();
            _shotTimer = _delayBeetweenShots;
        }

        if (_shotTimer > 0)
            _shotTimer -= Time.deltaTime;
    }
}
