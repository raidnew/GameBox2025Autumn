using System;

public interface IHealth
{
    public Action Died { get; set; }

    float HealthPoints { get; set; }

    void Damage(float damageValue);
   
}