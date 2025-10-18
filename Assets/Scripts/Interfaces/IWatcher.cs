using UnityEngine;

public interface IWatcher
{
    Vector3 LookDirection { get; set; }

    void Look(Vector2 direction);
}

