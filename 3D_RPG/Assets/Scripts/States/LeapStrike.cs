using UnityEngine;

public class LeapStrike : TargetAttack
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private float _elapsedTime = 0f;

    public override void EnterState()
    {
        if (null == _targetManager.EnemyTarget) return;

        base.EnterState();

        _elapsedTime = 0f;

        _startPosition = transform.position;

        _target = _targetManager.EnemyTarget;

        _targetPosition = _target.position - transform.forward * 2;
    }

    public override void FixedUpdateState()
    {
        if (null != _target)
        {
            _targetPosition = _target.position - transform.forward * 2;
        }

        _elapsedTime += Time.deltaTime;

        float normalizedElapsedTime = Mathf.Clamp(_elapsedTime / _attackDelayTime, 0f, 1f);

        transform.position = Vector3.Lerp(_startPosition, _targetPosition, normalizedElapsedTime);
    }

    public override void ExitState()
    {
        _startPosition = _targetPosition = transform.position;
    }
}