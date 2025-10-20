using UnityEngine;

public interface IGun
{
    float DamageValue { get; }
    GunType GetGunType();
    void TriggerOn();
    void TriggerOff();
    Vector3 Muzzle();

}