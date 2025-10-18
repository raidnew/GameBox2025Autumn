using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackerLo : MonoBehaviour, IWatcher
{
    [SerializeField] private CinemachineFreeLook _thirdPersonCamera;
    [SerializeField] private CinemachineFreeLook _firstPersonCamera;

    public Vector3 LookDirection { get; set; }

    public void Look(Vector2 direction)
    {
        
    }

    private void FixedUpdate()
    {
        CalcForward();
    }

    private void CalcForward()
    {
        LookDirection = transform.position - _thirdPersonCamera.transform.position;
        LookDirection.Normalize();
    }
}
