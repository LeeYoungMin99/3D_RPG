using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _delay = 0.2f;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private int _targetCount = 16;

    private Collider[] _targetColliders;
    private PlayerRotator _rotator;
    private TargetManager _targetManager;

    private void Start()
    {
        _targetColliders = new Collider[_targetCount];
        _rotator = GetComponent<PlayerRotator>();
        _targetManager = transform.parent.GetComponent<TargetManager>();
    }

    public override void EnterState()
    {
        if (null == _targetManager.Target)
        {
            return;
        }

        StartCoroutine(GiveDamage());
        StartCoroutine(InitializeLocalPositionAtEndOfFrame());
        StartCoroutine(_rotator.LookAtTargetAtEndOfFrame(_targetManager.Target));
    }

    public override void ExitState()
    {
        StartCoroutine(_rotator.RotateToTargetRotationAtEndOfFrame());
    }

    public override IEnumerator GiveDamage()
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
