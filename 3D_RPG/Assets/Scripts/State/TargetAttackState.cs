using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttackState : AttackState
{
    public override void EnterState()
    {
        if (null == _targetManager.EnemyTarget) return;

        base.EnterState();

        StartCoroutine(Attack());
    }

    protected override IEnumerator Attack()
    {
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        _targetManager.EnemyTarget.GetComponent<Status>().TakeDamage(_status.ATK);
    }
}
