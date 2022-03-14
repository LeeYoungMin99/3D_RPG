using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackState : AttackState
{
    [Range(0f, 180f)]
    [SerializeField] private float _horizontalAngle = 0f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private int _targetCount = 16;
    [SerializeField] private LayerMask _targetMask;

    private Collider[] _targetColliders;
    private Vector3 _forward;

    protected override void Start()
    {
        base.Start();

        _targetColliders = new Collider[_targetCount];
    }

    private IEnumerator GetForwardVectorFromEndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        _forward = transform.forward;
    }

    public override void EnterState()
    {
        StartCoroutine(InitializeLocalPositionAtEndOfFrame());

        if (null != _targetManager.EnemyTarget)
        {
            StartCoroutine(_rotator.LookAtTargetAtEndOfFrame(_targetManager.EnemyTarget.position));
        }
        else
        {
            StartCoroutine(_rotator.RotateToTargetRotationAtEndOfFrame());
        }

        StartCoroutine(GetForwardVectorFromEndOfFrame());
        StartCoroutine(Attack());
    }

    public override void ExitState()
    {
        StartCoroutine(InitializeLocalPositionAtEndOfFrame());
        StartCoroutine(_rotator.RotateToTargetRotationAtEndOfFrame());
    }

    public override IEnumerator Attack()
    {
        if (_delay > 0f)
        {
            yield return new WaitForSeconds(_delay);
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }

        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _targetColliders, _targetMask);

        Vector3 targetDir;

        for (int i = 0; i < targetCount; ++i)
        {
            Vector3 targetPosition = _targetColliders[i].transform.position;
            targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);

            Vector3 myPosition = new Vector3(transform.position.x, 0f, transform.position.z);

            targetDir = (targetPosition - myPosition).normalized;

            float dot = Vector3.Dot(targetDir, _forward);

            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= _horizontalAngle)
            {
                _targetColliders[i].GetComponent<Status>().TakeDamage(_status.ATK);
            }

            Debug.Log("tlqkf");
        }
    }
}
