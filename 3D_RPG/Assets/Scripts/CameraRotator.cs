using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    private float _topClamp = 70f;
    private float _bottomClamp = -30f;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    public GameObject CinemachineCameraTarget;
    public bool LockCameraPosition = false;

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 mouseMove = new Vector2(_input.MoveMouseX, _input.MoveMouseY);

        if (false == LockCameraPosition)
        {
            _cinemachineTargetYaw += mouseMove.x;
            _cinemachineTargetPitch -= mouseMove.y;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
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
