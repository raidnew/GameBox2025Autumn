using UnityEngine;

public class GrenadeLauncher : BaseGun, IGrenadeLauncher
{
    [SerializeField] private float _throwPower = 200;
    [SerializeField] private Transform _pointStart;
    [SerializeField] private GameObject _grenadePrefab;

    public override GunType GetGunType() => GunType.Grenade;

    public void Launch()
    {
        RaycastHit shothit;
        GameObject grenade = Instantiate(_grenadePrefab, _pointStart.position, Quaternion.identity);
        grenade.transform.parent = LevelManager.LevelObjectsContainer;
        if (GetShotDirection(out shothit))
        {
            Vector3 direction = shothit.point - _pointStart.position;
            Vector3 axis = Vector3.Cross(direction, Vector3.up);
            Quaternion turnUp = Quaternion.AngleAxis(45, axis);
            direction = turnUp * direction;
            grenade.GetComponent<Rigidbody>().AddForce(direction * _throwPower);
        }
    }

    public override Vector3 Muzzle()
    {
        return _pointStart.position;
    }
}
