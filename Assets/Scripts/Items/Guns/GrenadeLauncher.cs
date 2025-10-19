using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour, IGrenadeLauncher
{
    [SerializeField] private Transform _pointStart;
    [SerializeField] private GameObject _grenadePrefab;

    public void Launch()
    {
        GameObject grenade = Instantiate(_grenadePrefab, _pointStart.position, Quaternion.identity);
        grenade.transform.parent = LevelManager.LevelObjectsContainer;
    }
}
