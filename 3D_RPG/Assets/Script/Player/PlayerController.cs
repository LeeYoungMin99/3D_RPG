using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _characterBody;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;

    private void Update()
    {
        LookAround();

        Move();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(_input.HorizontalInput, _input.VerticalInput);

        _animator.SetFloat("Move", moveInput.magnitude);

        if (moveInput.magnitude != 0)
        {

            Vector3 lookForward = new Vector3(_cameraPoint.forward.x, 0f, _cameraPoint.forward.z).normalized;
            Vector3 lookRight = new Vector3(_cameraPoint.right.x, 0f, _cameraPoint.right.z).normalized;

            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            _characterBody.forward = moveDir;

            transform.position += moveDir * Time.deltaTime * _moveSpeed;
        }
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(_input.MouseXMove, _input.MouseYMove);
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
