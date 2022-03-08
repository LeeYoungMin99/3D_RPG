using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttackState : AttackState
{
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

        Debug.Log("데미지를 입히다");
    }
}
