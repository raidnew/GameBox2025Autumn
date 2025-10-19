using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletHoleManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHoleDecalPrefab;
    [SerializeField] private Transform _bulletHoleContainer;
    [SerializeField] private int _maxCountBulletHole = 10;

    private List<GameObject> _bulletHoles = new List<GameObject>();

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

    private void OnBulletHole(RaycastHit hit)
    {
        _bulletHoles.Add(NewBulletHole(hit));
        if(_bulletHoles.Count > _maxCountBulletHole)
        {
            GameObject removedHole = _bulletHoles.First();
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
            GameObject hole = _bulletHoles[i];
            if (hole.transform.parent == removedObject.transform)
            {
                Destroy(hole);
                _bulletHoles.RemoveAt(i);
            }

        }
    }
}
