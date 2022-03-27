using System;
using System.Collections;
using UnityEngine;

public abstract class AttackState : State
{
    [Header("Combo")]
    [SerializeField] protected bool _isCombo = false;
    [Range(0f, 1f)]
    [SerializeField] protected float _impossibleComboInputAnimationNormalizedTime = 0f;

    [Header("Skill")]
    [SerializeField] protected bool _isSkill = false;
    [Range(0f, 30f)]
    [SerializeField] protected float _cooldown = 0f;

    [Header("Attack Setting")]
    [Range(0f, 3f)]
    [SerializeField] protected float _attackDelayTime = 0.2f;

    protected CharacterRotator _rotator;
    protected CharacterStatus _status;
    protected Animator _animator;

    public event EventHandler<EventArgs> OnCooldownElapsed;

    public bool IsSkill { get { return _isSkill; } }

    protected virtual void Start()
    {
        _rotator = GetComponent<CharacterRotator>();
        _status = GetComponent<CharacterStatus>();
        _animator = GetComponent<Animator>();
    }

    protected float CalculateAngle(Vector3 myPosition, Vector3 targetPosition)
    {
        Vector3 targetDir = (targetPosition - myPosition).normalized;

        float dot = Mathf.Clamp(Vector3.Dot(targetDir, transform.forward), -1f, 1f);

        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        Vector3 cross = Vector3.Cross(transform.forward, targetDir);

        if (0f > cross.y)
        {
            angle *= -1;
        }

        return angle;
    }

    protected bool CheckComboPossible()
    {
        if (false == _isCombo) return false;

        if (_impossibleComboInputAnimationNormalizedTime > _animator.GetCurrentAnimatorStateInfo(0).normalizedTime) return false;

        return true;
    }

    protected IEnumerator CheckCooldown()
    {
        yield return new WaitForSeconds(_cooldown);

        _animator.SetBool(CharacterAnimID.IS_COOLDOWN, false);
        OnCooldownElapsed?.Invoke(this, EventArgs.Empty);
    }

    protected abstract IEnumerator Attack();

    public override void EnterState()
    {
        if (true == _isSkill)
        {
            StartCoroutine(CheckCooldown());

            _animator.SetBool(CharacterAnimID.IS_COOLDOWN, true);
        }

        if (null == _targetManager.EnemyTarget) return;

        StartCoroutine(Attack());

        Vector3 targetPosition = _targetManager.EnemyTarget.position;
        targetPosition.y = 0f;

        Vector3 myPosition = transform.position;
        myPosition.y = 0f;

        float angle = CalculateAngle(myPosition, targetPosition);

        _rotator.RotateImmediately(angle);
    }

    public override void UpdateState()
    {
        if (false == CheckComboPossible()) return;

        if (true == _isPlayableCharacter)
        {
            if (true == _playerInput.Attack)
            {
                _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

                return;
            }

            if (false == _isAuto) return;
        }

        if (null != _targetManager.EnemyTarget)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);
        }
    }
}
