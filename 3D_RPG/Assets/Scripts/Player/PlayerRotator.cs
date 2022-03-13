using System.Collections;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _rotationSmoothTime = 0.12f;

    private float _rotationVelocity;
    private float _targetRotation = 0f;

    private void RotateToAngle(float targetAngle)
    {
        _body.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
    }

    public void Rotate(float Angle, Transform cameraPoint)
    {
        _targetRotation = Angle * Mathf.Rad2Deg + cameraPoint.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(_body.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        RotateToAngle(rotation);
    }

    public IEnumerator RotateToTargetRotationAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        _body.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);

        RotateToAngle(_targetRotation);
    }

    public IEnumerator LookAtTargetAtEndOfFrame(Vector3 target)
    {
        yield return new WaitForEndOfFrame();

        Vector3 targetDir = target - transform.position;
        _targetRotation = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;

        RotateToAngle(_targetRotation);
    }
}
