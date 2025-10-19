using UnityEngine;

public class BaseGun : MonoBehaviour, IGun
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _shotEffect;

    public Vector3 Muzzle => _muzzle.position;

    public void Shot()
    {
        _shotEffect.Play();
    }
}
