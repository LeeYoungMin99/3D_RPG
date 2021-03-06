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

    private static readonly float TOP_CLAMP = 70f;
    private static readonly float BOTTOM_CLAMP = -30f;

    public Transform CinemachineCameraTarget { set { _cinemachineCameraTarget = value; } }
    public bool LockCameraPosition { set { _lockCameraPosition = value; } }

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
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BOTTOM_CLAMP, TOP_CLAMP);

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
