using System;
using System.Collections;
using UnityEngine;

public abstract class AttackState : State, IExperienceGainer
{
    [Header("Combo")]
    [SerializeField] protected bool _isCombo = false;
    [Range(0f, 1f)]
    [SerializeField] protected float _impossibleComboInputAnimationNormalizedTime = 0f;
    [Range(0f, 15f)]
    [SerializeField] protected float _comboAttackDistance = 2f;

    [Header("Skill")]
    [SerializeField] protected bool _isSkill = false;
    [Range(0f, 30f)]
    [SerializeField] protected float _cooldownTime = 0f;
    [SerializeField] protected Sprite _skillIcon;

    [Header("Camera Shake")]
    [Range(0f, 2f)]
    [SerializeField] protected float _shakeTime = 0f;
    [Range(0f, 10f)]
    [SerializeField] protected float _amplitueGain = 0f;

    [Header("Attack Setting")]
    [Range(0f, 10f)]
    [SerializeField] protected float _attackDelayTime = 0.2f;

    private SkillEventArgs _eventArgs = new SkillEventArgs();

    protected CharacterRotator _rotator;
    protected CharacterStatus _status;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected Transform _target;

    public event EventHandler<EventArgs> OnCooldownTimeElapsedEvent;
    public event EventHandler<SkillEventArgs> UseSkillEvent;

    protected static readonly Vector3 ZERO_VECTOR3 = Vector3.zero;

    public bool IsSkill { get { return _isSkill; } }
    public Sprite SkillIcon { get { return _skillIcon; } }

    protected override void Awake()
    {
        base.Awake();

        _rotator = GetComponent<CharacterRotator>();
        _status = GetComponent<CharacterStatus>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected bool CheckComboPossible()
    {
        if (false == _isCombo) return false;

        if (_impossibleComboInputAnimationNormalizedTime > _animator.GetCurrentAnimatorStateInfo(0).normalizedTime) return false;

        return true;
    }

    protected IEnumerator StartCooldown()
    {
        _eventArgs.CooldownTime = _cooldownTime;
        UseSkillEvent?.Invoke(this, _eventArgs);

        yield return new WaitForSeconds(_cooldownTime);

        _animator.SetBool(CharacterAnimID.IS_COOLDOWN, false);
        OnCooldownTimeElapsedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected abstract IEnumerator Attack();

    public override void EnterState()
    {
        if (true == _isSkill)
        {
            StartCoroutine(StartCooldown());

            _animator.SetBool(CharacterAnimID.IS_COOLDOWN, true);
        }

        if (null == _targetManager.EnemyTarget) return;

        StartCoroutine(Attack());

        Vector3 targetPosition = _targetManager.EnemyTarget.position;

        float angle = Utils.Instance.CalculateAngle(transform, targetPosition);

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

        if (null == _targetManager.EnemyTarget) return;

        float curDistance = Vector3.Distance(transform.position, _targetManager.EnemyTarget.position);

        if (curDistance >= _comboAttackDistance) return;

        _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);
    }

    public override void ExitState()
    {
        _rigidbody.velocity = ZERO_VECTOR3;
    }

    public void UseSkill()
    {
        _animator.SetTrigger(CharacterAnimID.USE_SKILL);
    }

    public void GainExperience(object sender, DeathEventArgs args)
    {
        _status.ObtainExperience(args.Experience);
    }
}
