using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttackState : AttackState
{
    protected override IEnumerator Attack()
    {
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        _targetManager.EnemyTarget.GetComponent<CharacterStatus>().TakeDamage(_status.ATK);
    }
}
