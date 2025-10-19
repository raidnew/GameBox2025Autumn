using System;
using UnityEngine;

public class BaseGun : MonoBehaviour, IGun
{
    public static Action<RaycastHit> BulletHit;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private float _damageValue;

    public Vector3 Muzzle => _muzzle.position;

    public float DamageValue { get; set; }

    private void Awake()
    {
        DamageValue = _damageValue;
    }

    public virtual void TriggerOff()
    {
    }

    public virtual void TriggerOn()
    {
        Hammer();
    }

    protected void Hammer()
    {
        _shotEffect.Play();
        RaycastHit viewhit, shothit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out viewhit))
        {
            Ray shootRay = new Ray(Muzzle, viewhit.point - Muzzle);
            if (Physics.Raycast(shootRay, out shothit))
            {
                BulletHit?.Invoke(shothit);
                IDestroyed hittedObject;
                if(shothit.collider.TryGetComponent<IDestroyed>(out hittedObject))
                    hittedObject.Damage(DamageValue);
            }
        }
    }
}
