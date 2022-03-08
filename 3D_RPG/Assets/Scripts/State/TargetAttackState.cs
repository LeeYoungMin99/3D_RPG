using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttackState : AttackState
{
    private PlayerRotator _rotator;
    private TargetManager _targetManager;

    private void Start()
    {
        _rotator = GetComponent<PlayerRotator>();
        _targetManager = transform.parent.GetComponent<TargetManager>();
    }

    public override void EnterState()
    {
        StartCoroutine(InitializeLocalPositionAtEndOfFrame());

        if (null == _targetManager.Target)
        {
            return;
        }

        StartCoroutine(GiveDamage());
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

        //_targetManager.Target
        Debug.Log("데미지를 입히다");
    }
}
