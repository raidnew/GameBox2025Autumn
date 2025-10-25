using System;
using UnityEngine;

public class DestoyableObject : MonoBehaviour, IDestroyed
{
    [SerializeField] private Health health;

    public static Action<GameObject> Crush { get; set; }

    private void OnEnable()
    {
        health.Died += OnDied;
    }

    private void OnDisable()
    {
        health.Died -= OnDied;
    }

    public void Damage(float value)
    {
        health.Damage(value);
    }

    private void OnDied()
    {
        Crush?.Invoke(gameObject);
        ActionOnDied();
    }

    protected virtual void ActionOnDied()
    {
        Destroy(gameObject);
    }
}
