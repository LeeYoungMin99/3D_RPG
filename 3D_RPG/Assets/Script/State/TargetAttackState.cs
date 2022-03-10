using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttackState : AttackState
{
    [SerializeField] LineRenderer _effect;
    [SerializeField] float _effectLifeTime = 0.2f;
    [SerializeField] Transform _effectStartPosition;

    private IEnumerator AttackEffect(Vector3 target)
    {
        _effect.enabled = true;

        _effect.SetPosition(0, _effectStartPosition.position);
        _effect.SetPosition(1, target);

        yield return new WaitForSeconds(_effectLifeTime);

        _effect.enabled = false;
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

        Vector3 target = _targetManager.Target.position;

        StartCoroutine(AttackEffect(target));

        _targetManager.Target.GetComponent<Status>().TakeDamage(_status.ATK);
    }
}
