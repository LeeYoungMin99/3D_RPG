using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRotator : MonoBehaviour
{
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private PlayerInput _input;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector2 mouseDelta = new Vector2(_input.MoveMouseX, _input.MoveMouseY);
        Vector3 camAngle = _cameraPoint.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        _cameraPoint.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
