using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierThrownObject : ThrownObject
{
    [Header("Attack Setting")]
    [SerializeField] private float _targetTime = 1f;
    [SerializeField] private float _maxRandomDistance = 1f;

    private Vector3[] _bezierCurvePoints = new Vector3[4];
    private float _elapsedTime = 0f;

    private void OnEnable()
    {
        _elapsedTime = 0f;

        InitBezierCurvePoint();

        EnableFlyingEffect();
    }

    private void FixedUpdate()
    {
        if (_elapsedTime >= _targetTime) return;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _targetTime)
        {
            EnableExplosionEffect();
        }

        float normalizedElapsedTime = Mathf.Clamp01(_elapsedTime / _targetTime);

        _bezierCurvePoints[3] = Target.position + CORRECT_TARGET_POSITION_VECTOR;

        transform.position = new Vector3(
            CalculateBezierCurve(_bezierCurvePoints[0].x, _bezierCurvePoints[1].x, _bezierCurvePoints[2].x, _bezierCurvePoints[3].x, normalizedElapsedTime),
            CalculateBezierCurve(_bezierCurvePoints[0].y, _bezierCurvePoints[1].y, _bezierCurvePoints[2].y, _bezierCurvePoints[3].y, normalizedElapsedTime),
            CalculateBezierCurve(_bezierCurvePoints[0].z, _bezierCurvePoints[1].z, _bezierCurvePoints[2].z, _bezierCurvePoints[3].z, normalizedElapsedTime));
    }

    private void InitBezierCurvePoint()
    {
        float randomPositionX = Random.Range(-1.0f, 1.0f);
        float randomPositionY = Random.Range(-0.5f, 1.0f);

        _bezierCurvePoints[0] = Owner.position;

        _bezierCurvePoints[1] = Owner.position
            + (_maxRandomDistance * randomPositionX * Owner.right)
            + (_maxRandomDistance * randomPositionY * Owner.up);

        _bezierCurvePoints[2] = Target.position + (_maxRandomDistance * randomPositionX * Owner.right);

        _bezierCurvePoints[3] = Target.position + CORRECT_TARGET_POSITION_VECTOR;

        transform.position = Owner.position;
    }

    private float CalculateBezierCurve(float a, float b, float c, float d, float normalizedElapsedTime)
    {
        return Mathf.Pow((1 - normalizedElapsedTime), 3) * a
            + Mathf.Pow((1 - normalizedElapsedTime), 2) * 3 * normalizedElapsedTime * b
            + Mathf.Pow(normalizedElapsedTime, 2) * 3 * (1 - normalizedElapsedTime) * c
            + Mathf.Pow(normalizedElapsedTime, 3) * d;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target.gameObject)
        {
            other.GetComponent<CharacterStatus>().TakeDamage(Damage);
        }
    }
}

