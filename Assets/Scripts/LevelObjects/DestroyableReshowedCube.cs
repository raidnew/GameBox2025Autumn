using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyableReshowedCube : DestoyableObject
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _container;

    private Vector3 _position;
    protected override void ActionOnDied()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        _position = transform.position;
        StartCoroutine(DelayGeneration());
    }

    private void CreateNewCube()
    {
        GameObject newCube = Instantiate(_cubePrefab, _position, Quaternion.identity, _container.transform);
        LevelManager.AddObjectOnLevel(newCube);
        Renderer renderer = newCube.GetComponent<Renderer>();
        renderer.enabled = true;
        Destroy(gameObject);
    }

    private IEnumerator DelayGeneration()
    {
        yield return new WaitForSeconds(3);
        CreateNewCube();
        yield return true;
    }
}
