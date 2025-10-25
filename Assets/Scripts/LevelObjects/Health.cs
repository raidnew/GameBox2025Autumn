using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private float StartPoints;

    public Action Died { get; set; }
    public float HealthPoints { get; set; }

    private void Awake()
    {
        HealthPoints = StartPoints;
    }

    public void Damage(float damageValue)
    {
        HealthPoints -= damageValue;
        if(HealthPoints <= 0)
        {
            HealthPoints = 0;
            Died?.Invoke();
        }
    }
}
