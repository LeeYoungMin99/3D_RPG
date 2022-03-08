using System.Collections;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _rotationSmoothTime = 0.12f;

    private float _rotationVelocity;

    public float TargetRotation { get; private set; } = 0f;

    private void Rotate(float targetAngle)
    {
        _body.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
    }

    public void Rotate(Vector2 input, Transform cameraPoint)
    {
        TargetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraPoint.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(_body.eulerAngles.y, TargetRotation, ref _rotationVelocity, _rotationSmoothTime);

        Rotate(rotation);
    }

    public IEnumerator RotateToTargetRotation()
    {
        yield return new WaitForEndOfFrame();

        _body.rotation = Quaternion.Euler(0.0f, TargetRotation, 0.0f);

        Rotate(TargetRotation);
    }

    public IEnumerator LookatTarget(Transform target)
    {
        yield return new WaitForEndOfFrame();

        Vector3 targetDir = target.position - transform.position;

        TargetRotation = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;

        Rotate(TargetRotation);
    }
}
