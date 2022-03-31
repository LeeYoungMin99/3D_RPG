using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttack : AttackState
{
    protected override IEnumerator Attack()
    {
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        StartCoroutine(CinemachineShaker.Instance.ShakeCamera(_amplitueGain, _shakeTime));

        _targetManager.EnemyTarget?.GetComponent<CharacterStatus>()?.TakeDamage(_status.ATK, GainExperience);
    }
}
