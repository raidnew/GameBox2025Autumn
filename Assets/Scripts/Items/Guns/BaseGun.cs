using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour, IGun
{
    [SerializeField] private Transform _muzzle;
    public Vector3 Muzzle => _muzzle.position;

    public float DamageValue => throw new System.NotImplementedException();

    public virtual void TriggerOff()
    {
        
    }

    public virtual void TriggerOn()
    {
        
    }
}
