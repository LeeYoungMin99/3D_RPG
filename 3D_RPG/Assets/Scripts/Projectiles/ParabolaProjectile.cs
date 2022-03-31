using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaProjectile : BezierProjectile
{
    [SerializeField] private float _hitRadius;
    [SerializeField] private int _targetCount = 16;
    [Space(10f)]
    [SerializeField] private GameObject _rangeCircle;


    private Collider[] _targetColliders;

    private static readonly Vector3 CORRECT_CIRCLE_POSITION_VECTOR = new Vector3(0f, 0.3f, 0f);

    protected override void Awake()
    {
        base.Awake();

        _targetColliders = new Collider[_targetCount];

        _rangeCircle = Instantiate(_rangeCircle, transform.parent);
        _rangeCircle.transform.localScale *= _hitRadius;
    }

    protected override void OnEnable()
    {
        SetTrailRendererDisplay(true);
        SetFlyingEffectDisplay(true);
        SetExplosionEffectDisplay(false);

        _elapsedTime = 0f;

        transform.position = StartPosition.position;

        Vector3 heightVector = new Vector3(0f, _bezierCurvePointDistance, 0f);

        Vector3 secondPoint = StartPosition.position
            + (Target.position - StartPosition.position) / 2
            + heightVector;

        InitBezierCurvePoint(
            StartPosition.position,
            secondPoint,
            Target.position + heightVector,
            Target.position);

        _rangeCircle.transform.position = Target.position + CORRECT_CIRCLE_POSITION_VECTOR;
        _rangeCircle.SetActive(true);
    }

    protected override void FixedUpdate()
    {
        if (_elapsedTime >= _targetTime) return;

        UpdatePosition();

        if (_elapsedTime >= _targetTime)
        {
            SetTrailRendererDisplay(false);
            SetFlyingEffectDisplay(false);
            SetExplosionEffectDisplay(true);
            _rangeCircle.SetActive(false);

            int targetCount = Physics.OverlapSphereNonAlloc(_bezierCurvePoints[3], _hitRadius, _targetColliders, TargetLayer);

            StartCoroutine(CinemachineShaker.Instance.ShakeCamera(_amplitueGain, _shakeTime));

            if (0 == targetCount) return;

            for (int i = 0; i < targetCount; ++i)
            {
                _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(Damage, GainExperience);
            }
        }
    }
}

