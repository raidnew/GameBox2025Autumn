using UnityEngine;

public interface IGun
{
    Vector3 Muzzle { get; }
    float DamageValue { get; }

    void TriggerOn();
    void TriggerOff();
}