using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    private float _rotationVelocity;
    private float _rotationSmoothTime = 0.06f;

    public void RotateSmoothly(float angle)
    {
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _rotationVelocity, _rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }

    public void RotateImmediately(float angle)
    {
        transform.eulerAngles += new Vector3(0.0f, angle, 0.0f);
    }
}
