using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHoleDecalPrefab;
    [SerializeField] private Transform _bulletHoleContainer;

    private List<GameObject> _bulletHoles = new List<GameObject>();

    private void OnEnable()
    {
        CharacterGunsUser.BulletHit += OnBulletHole;
    }

    private void OnDisable()
    {
        CharacterGunsUser.BulletHit -= OnBulletHole;
    }

    private void OnBulletHole(RaycastHit hit)
    {
        GameObject newBulletHole = Instantiate(_bulletHoleDecalPrefab, _bulletHoleContainer, true);
        newBulletHole.transform.position = hit.point;
        newBulletHole.transform.rotation = Quaternion.LookRotation(hit.normal);

        Debug.DrawRay(hit.point, hit.normal * 10, Color.red, 10f);

        _bulletHoles.Add(newBulletHole);
    }
}
