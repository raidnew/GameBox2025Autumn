using System;
using UnityEngine;

public class BaseGun : MonoBehaviour, IGun
{
    public static Action<RaycastHit> BulletHit;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _shotEffect;

    public Vector3 Muzzle => _muzzle.position;

    public void Shot()
    {
        _shotEffect.Play();

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit viewhit;
        RaycastHit shothit;
        if (Physics.Raycast(ray, out viewhit))
        {
            Ray shootRay = new Ray(Muzzle, viewhit.point - Muzzle);
            if (Physics.Raycast(shootRay, out shothit))
            {
                //Debug.DrawLine(_currentGun.Muzzle, shothit.point, Color.red, 2f);
                BulletHit?.Invoke(shothit);
            }
        }
    }
}
