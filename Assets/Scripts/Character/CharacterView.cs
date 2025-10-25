using Cinemachine;
using UnityEngine;

public class CharacterView : MonoBehaviour, IWatcher
{
    [SerializeField] private CharacterInput _input;
    [SerializeField] private CinemachineVirtualCameraBase _thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCameraBase _firstPersonCamera;
    [SerializeField] private float _speedRotation = 0.01f;

    private CinemachineVirtualCameraBase _currentCamera;
    private bool _calcLookByCam;
    private Vector2 _lookMove;

    public Vector3 LookDirection { get; private set; }

    private void OnEnable()
    {
        LookThirdPerson();
        _input.SwitchView += OnSwitchView;
        _input.LookMove += OnLook;
    }

    private void OnDisable()
    {
        _input.SwitchView -= OnSwitchView;
        _input.LookMove -= OnLook;
    }

    private void FixedUpdate()
    {
        if (_calcLookByCam) CalcForwardByCamera();
        else CalcViewByVector();
    }

    private void OnSwitchView(ViewType type)
    {
        switch (type)
        {
            case ViewType.First:
                LookFirstPerson();
                break;
            case ViewType.Third:
                LookThirdPerson();
                break;
        }
    }

    private void LookFirstPerson()
    {
        if (_currentCamera == _firstPersonCamera) return;
        SetCurrentCamera(_firstPersonCamera);
        _calcLookByCam = false;
    }

    private void LookThirdPerson()
    {
        if (_currentCamera == _thirdPersonCamera) return;
        SetCurrentCamera(_thirdPersonCamera);
        _calcLookByCam = true;
    }
    private void OnLook(Vector2 direction)
    {
        _lookMove = direction;
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
