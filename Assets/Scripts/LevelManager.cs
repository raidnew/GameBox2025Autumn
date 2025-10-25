using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Transform LevelObjectsContainer;

    [SerializeField] private Transform _levelObjectsContainer;

    public static void AddObjectOnLevel(GameObject newObject)
    {
        newObject.transform.parent = LevelObjectsContainer;
    }

    private void Awake()
    {
        LevelObjectsContainer = _levelObjectsContainer;
    }
}
