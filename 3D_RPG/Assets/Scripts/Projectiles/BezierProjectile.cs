using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierProjectile : Projectile
{
    [Header("Curve Setting")]
    [SerializeField] protected float _targetTime = 1f;
    [SerializeField] protected float _bezierCurvePointDistance = 1f;

    protected Vector3[] _bezierCurvePoints = new Vector3[4];
    protected float _elapsedTime = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();

        _elapsedTime = 0f;

        float randomPositionX = Random.Range(-1.0f, 1.0f);
        float randomPositionY = Random.Range(-0.5f, 1.0f);

        Vector3 secondPoint = StartPosition.position
        + (_bezierCurvePointDistance * randomPositionX * StartPosition.right)
        + (_bezierCurvePointDistance * randomPositionY * StartPosition.up);

        Vector3 thirdPoint = Target.position
            + (_bezierCurvePointDistance * randomPositionX * StartPosition.right);

        InitBezierCurvePoint(
            StartPosition.position,
            secondPoint,
            thirdPoint,
            Target.position + CORRECT_TARGET_POSITION_VECTOR);
    }

    protected virtual void FixedUpdate()
    {
        if (_elapsedTime >= _targetTime) return;

        _bezierCurvePoints[3] = Target.position + CORRECT_TARGET_POSITION_VECTOR;

        UpdatePosition();

        if (_elapsedTime >= _targetTime)
        {
            SetTrailRendererDisplay(false);
            SetFlyingEffectDisplay(false);
            SetExplosionEffectDisplay(true);

            Target.GetComponent<CharacterStatus>().TakeDamage(Damage, GainExperience);
        }
    }

    protected void InitBezierCurvePoint(Vector3 firstPoint, Vector3 secondPoint, Vector3 thirdPoint, Vector3 fourthPoint)
    {
        _bezierCurvePoints[0] = firstPoint;

        _bezierCurvePoints[1] = secondPoint;

        _bezierCurvePoints[2] = thirdPoint;

        _bezierCurvePoints[3] = fourthPoint;
    }

    protected void UpdatePosition()
    {
        _elapsedTime += Time.deltaTime;

        float normalizedElapsedTime = Mathf.Clamp01(_elapsedTime / _targetTime);

        transform.position = new Vector3(
            CalculateBezierCurve(_bezierCurvePoints[0].x, _bezierCurvePoints[1].x, _bezierCurvePoints[2].x, _bezierCurvePoints[3].x, normalizedElapsedTime),
            CalculateBezierCurve(_bezierCurvePoints[0].y, _bezierCurvePoints[1].y, _bezierCurvePoints[2].y, _bezierCurvePoints[3].y, normalizedElapsedTime),
            CalculateBezierCurve(_bezierCurvePoints[0].z, _bezierCurvePoints[1].z, _bezierCurvePoints[2].z, _bezierCurvePoints[3].z, normalizedElapsedTime));
    }

    protected float CalculateBezierCurve(float a, float b, float c, float d, float normalizedElapsedTime)
    {
        return Mathf.Pow((1 - normalizedElapsedTime), 3) * a
            + Mathf.Pow((1 - normalizedElapsedTime), 2) * 3 * normalizedElapsedTime * b
            + Mathf.Pow(normalizedElapsedTime, 2) * 3 * (1 - normalizedElapsedTime) * c
            + Mathf.Pow(normalizedElapsedTime, 3) * d;
    }
}

