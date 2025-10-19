using UnityEngine;

public interface IGun
{
    Vector3 Muzzle { get; }

    void TriggerOn();
    void TriggerOff();
}