using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private int _targetCount = 16;

    private Collider[] _targetColliders;

    protected override void Start()
    {
        base.Start();

        _targetColliders = new Collider[_targetCount];
    }

    public override void EnterState()
    {
        StartCoroutine(InitializeLocalPositionAtEndOfFrame());

        if (null == _targetManager.Target)
        {
            return;
        }

        StartCoroutine(Attack());
        StartCoroutine(_rotator.LookAtTargetAtEndOfFrame(_targetManager.Target));
    }

    public override void ExitState()
    {
        StartCoroutine(_rotator.RotateToTargetRotationAtEndOfFrame());
    }

    public override IEnumerator Attack()
    {
        if (_delay > 0f)
        {
            yield return new WaitForSeconds(_delay);
        }

        int targetCount = Physics.OverlapSphereNonAlloc(_targetManager.Target.position, _radius, _targetColliders, _targetMask);

        for (int i = 0; i < targetCount; ++i)
        {
            Debug.Log("데미지를 입히다");
        }
    }
}
