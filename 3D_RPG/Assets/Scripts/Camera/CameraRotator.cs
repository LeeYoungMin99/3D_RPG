using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    private Transform _cinemachineCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private bool _lockCameraPosition = true;

    private static readonly float _topClamp = 70f;
    private static readonly float _bottomClamp = -30f;

    public Transform CinemachineCameraTarget 
    { 
        set 
        { 
            _cinemachineCameraTarget = value;
            _lockCameraPosition = false;
        } 
    }

    private void Update()
    {
        if (true == _input.CameraLock)
        {
            _lockCameraPosition = !_lockCameraPosition;
        }
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (true == _lockCameraPosition) return;

        Vector2 mouseMove = new Vector2(_input.MoveMouseX, _input.MoveMouseY);

        _cinemachineTargetYaw += mouseMove.x;
        _cinemachineTargetPitch -= mouseMove.y;

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

        _cinemachineCameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }

    private float ClampAngle(float angle, float lfMin, float lfMax)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        else if (angle > 360f)
        {
            angle -= 360f;
        }

        return Mathf.Clamp(angle, lfMin, lfMax);
    }
}
