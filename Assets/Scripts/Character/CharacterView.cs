using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CharacterView : MonoBehaviour, IWatcher
{
    [SerializeField] private CinemachineVirtualCameraBase _thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCameraBase _firstPersonCamera;
    [SerializeField] private float _speedRotation = 0.01f;

    private CinemachineVirtualCameraBase _currentCamera;
    private bool _calcLookByCam;
    private Vector2 _lookMove;

    public Vector3 LookDirection { get; set; }

    public void Look(Vector2 direction)
    {
        _lookMove = direction;
    }

    public void LookFirstPerson()
    {
        if (_currentCamera == _firstPersonCamera) return;
        SetCurrentCamera(_firstPersonCamera);
        _calcLookByCam = false;
    }

    public void LookThirdPerson()
    {
        if (_currentCamera == _thirdPersonCamera) return;
        SetCurrentCamera(_thirdPersonCamera);
        _calcLookByCam = true;
    }

    private void OnEnable()
    {
        LookThirdPerson();
    }

    private void FixedUpdate()
    {
        if (_calcLookByCam) CalcForwardByCamera();
        else CalcViewByVector();
    }

    private void CalcViewByVector()
    {
        LookDirection = Quaternion.Euler(0, _lookMove.x * _speedRotation, 0) * LookDirection;
    }

    private void CalcForwardByCamera()
    {
        if (_currentCamera == null) return;
        LookDirection = _currentCamera.LookAt.position - _currentCamera.transform.position;
        LookDirection.Normalize();
    }

    private void SetCurrentCamera(CinemachineVirtualCameraBase camera)
    {
        if(_currentCamera != null) _currentCamera.Priority = 0;
        _currentCamera = camera;
        _currentCamera.Priority = 10;
    }
}
