using System.Collections;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _rotationSmoothTime = 0.12f;

    private float _rotationVelocity;

    private float _targetRotation = 0f;

    private void Rotate(float targetAngle)
    {
        _body.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
    }

    public void Rotate(Vector2 input, Transform cameraPoint)
    {
        _targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraPoint.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(_body.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        Rotate(rotation);
    }

    public IEnumerator RotateToTargetRotationAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        _body.rotation = Quaternion.Euler(0.0f, _targetRotation, 0.0f);

        Rotate(_targetRotation);
    }

    public IEnumerator LookAtTargetAtEndOfFrame(Transform target)
    {
        yield return new WaitForEndOfFrame();

        Vector3 targetDir = target.position - transform.position;
        _targetRotation = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
        
        Rotate(_targetRotation);
    }
}
