using System;
using UnityEngine;

public class FirearmsGun : BaseGun
{
    public static Action<RaycastHit> BulletHit;

    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private ShotSoundEffect _shotSoundEffect;
    [SerializeField] private float _damageValue;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GunType _type;

    public override GunType GetGunType() => _type;

    private void Awake()
    {
        DamageValue = _damageValue;
    }

    public override void TriggerOn()
    {
        Hammer();
    }

    protected void Hammer()
    {
        _shotEffect.Play();
        _shotSoundEffect.Play();
        RaycastHit shothit;

        if(GetShotDirection(out shothit))
        {
            BulletHit?.Invoke(shothit);
            IDestroyed hittedObject;
            if (shothit.collider.TryGetComponent<IDestroyed>(out hittedObject))
                hittedObject.Damage(DamageValue);
        }
    }

    public override Vector3 Muzzle() => _muzzle.position;
}
