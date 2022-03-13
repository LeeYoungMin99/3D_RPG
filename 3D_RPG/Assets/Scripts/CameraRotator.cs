using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _rotationSmoothTime = 0.12f;

    private float _rotationVelocity;

    private void Update()
    {
        Rotate();
    }

    private void RotateToAngle(float targetAngle)
    {
        transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
    }

    private void Rotate()
    {
        Vector2 mouseDelta = new Vector2(_input.MoveMouseX, _input.MoveMouseY);
        Vector3 camAngle = transform.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 340f, 361f);
        }

        transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    public void RotateCameraAngleForAutoMove()
    {
        float _targetRotation = Mathf.Atan2(0, 1) * Mathf.Rad2Deg + _player.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        RotateToAngle(rotation);
    }
}
