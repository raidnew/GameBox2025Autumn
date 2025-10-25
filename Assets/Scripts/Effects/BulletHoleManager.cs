using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletHoleManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHoleDecalPrefab;
    [SerializeField] private Transform _bulletHoleContainer;
    [SerializeField] private int _maxCountBulletHole = 10;
    [SerializeField] private float _holesTimeToLive = 10;

    private List<BulletHoleData> _bulletHoles = new List<BulletHoleData>();

    private void OnEnable()
    {
        FirearmsGun.BulletHit += OnBulletHole;
        DestoyableObject.Crush += CheckHoleOnObject;
    }

    private void OnDisable()
    {
        FirearmsGun.BulletHit -= OnBulletHole;
        DestoyableObject.Crush -= CheckHoleOnObject;
    }

    private void Update()
    {
        CheckHoleOnTTL();
    }

    private void OnBulletHole(RaycastHit hit)
    {
        _bulletHoles.Add(new BulletHoleData { timeCreation = Time.time, _bulletHole = NewBulletHole(hit) });
        if(_bulletHoles.Count > _maxCountBulletHole)
        {
            GameObject removedHole = _bulletHoles.First()._bulletHole;
            _bulletHoles.RemoveAt(0);
            Destroy(removedHole);
        }
    }

    private GameObject NewBulletHole(RaycastHit hit)
    {
        GameObject newBulletHole = Instantiate(_bulletHoleDecalPrefab, hit.collider.transform, true);
        newBulletHole.transform.position = hit.point;
        newBulletHole.transform.rotation = Quaternion.LookRotation(hit.normal);
        return newBulletHole;
    }

    private void CheckHoleOnObject(GameObject removedObject)
    {
        for (int  i = _bulletHoles.Count - 1; i >= 0; i--)
        {
            GameObject hole = _bulletHoles[i]._bulletHole;
            if (hole.transform.parent == removedObject.transform)
            {
                Destroy(hole);
                _bulletHoles.RemoveAt(i);
            }
        }
    }

    private void CheckHoleOnTTL()
    {
        for (int i = _bulletHoles.Count - 1; i >= 0; i--)
        {
            if (_bulletHoles[i].timeCreation + _holesTimeToLive < Time.time)
            {
                Destroy(_bulletHoles[i]._bulletHole);
                _bulletHoles.RemoveAt(i);
            }
        }
    }

    private struct BulletHoleData
    {
        public GameObject _bulletHole;
        public float timeCreation;
    }
}

