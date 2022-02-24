using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _body;

    private float _rotationVelocity;
    private float _rotationSmoothTime = 0.12f;

    private float _targetRotation = 0f;
    public void Rotate(Vector2 input,Transform cameraPoint)
    {
        _targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraPoint.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(_body.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        _body.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
}
