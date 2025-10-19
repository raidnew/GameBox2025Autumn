using UnityEngine;

public interface IGun
{
    float DamageValue { get; }
    void TriggerOn();
    void TriggerOff();
    Vector3 Muzzle();
}