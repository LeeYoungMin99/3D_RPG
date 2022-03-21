using UnityEngine;

public class LeapStrikeState : TargetAttackState
{
    private Transform _target;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;

    private float _elapsedTime = 0f;

    public override void EnterState()
    {
        if (null == _targetManager.EnemyTarget)
        {
            _startPosition = _targetPosition = transform.position;

            return;
        };

        base.EnterState();

        _elapsedTime = 0f;
        _startPosition = transform.position;
        _target = _targetManager.EnemyTarget;
        _targetPosition = _target.position;
    }

    public override void UpdateState()
    {
        if (null != _target)
        {
            _targetPosition = _target.position;
        }

        _elapsedTime += Time.deltaTime;

        float clampElapsedTime = Mathf.Clamp(_elapsedTime / _delay, 0f, 0.8f);

        transform.position = Vector3.Lerp(_startPosition, _targetPosition, clampElapsedTime);

        base.UpdateState();
    }
}