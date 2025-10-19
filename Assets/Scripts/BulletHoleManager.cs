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
        BaseGun.BulletHit += OnBulletHole;
    }

    private void OnDisable()
    {
        BaseGun.BulletHit -= OnBulletHole;
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
        GameObject newBulletHole = Instantiate(_bulletHoleDecalPrefab, _bulletHoleContainer, true);
        newBulletHole.transform.position = hit.point;
        newBulletHole.transform.rotation = Quaternion.LookRotation(hit.normal);
        return newBulletHole;
    }
}
