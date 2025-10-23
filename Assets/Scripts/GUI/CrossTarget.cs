using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrossTarget : MonoBehaviour
{
    [SerializeField] private GameObject _gunnerGO;
    [SerializeField] private Image _crossTarget;
    private IGunsMan _gunner;
    private bool _active = false;

    private void Awake()
    {
        if (_gunnerGO.TryGetComponent<IGunsMan>(out _gunner))
        {
            _active = true;
        }
        OnGunnerDisarmed();
    }

    private void OnEnable()
    {
        if (!_active) return;
        _gunner.Armed += OnGunnerArmed;
        _gunner.DisArmed += OnGunnerDisarmed;
    }

    private void OnDisable()
    {
        if (!_active) return;
        _gunner.Armed -= OnGunnerArmed;
        _gunner.DisArmed -= OnGunnerDisarmed;
    }

    private void OnGunnerDisarmed()
    {
        _crossTarget.enabled = false;
    }

    private void OnGunnerArmed()
    {
        _crossTarget.enabled = true;
    }
}
