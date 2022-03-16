using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    private static readonly float THRESHOLD = 0.1f;

    private float _rotationVelocity;
    private float _rotationSmoothTime = 0.06f;


    public void RotateSmoothly(float angle)
    {
        if (true == float.IsNaN(angle)) return;
        if (THRESHOLD > Mathf.Abs(angle)) return;

        float rotation = angle * (Time.deltaTime / _rotationSmoothTime);

        transform.eulerAngles += new Vector3(0.0f, rotation, 0.0f);
    }

    public void RotateSmoothDampAngle(float angle)
    {
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _rotationVelocity, _rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }

    public void RotateImmediately(float angle)
    {
        if (THRESHOLD > Mathf.Abs(angle))
        {
            return;
        }

        transform.eulerAngles += new Vector3(0.0f, angle, 0.0f);
    }
}
