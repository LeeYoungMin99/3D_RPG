using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : State
{
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 3f;

    private GameObject _mainCamera;
    private CharacterRotator _rotator;
    private Rigidbody _rigidbody;
    private Coroutine _coroutineSetNavMeshPath;
    private float _animationBlend = 0f;

    protected Animator _animator;
    protected NavMeshAgent _navMeshAgent;
    protected bool _canUseSkill = true;

    protected const float PATH_FIND_DELAY = 0.25f;
    protected const float IDLE_ANIMATION_PARAMETER_VALUE = 0f;
    protected const float RUN_ANIMATION_PARAMETER_VALUE = 1f;

    private static readonly Vector2 ZERO_VECTOR2 = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        AttackState[] attackStates = GetComponents<AttackState>();

        _canUseSkill = false;
        _animator.SetBool(CharacterAnimID.IS_COOLDOWN, true);

        foreach (AttackState attackState in attackStates)
        {
            if (false == attackState.IsSkill) continue;

            attackState.OnCooldownTimeElapsedEvent -= ElapseCooldown;
            attackState.OnCooldownTimeElapsedEvent += ElapseCooldown;

            _canUseSkill = true;
            _animator.SetBool(CharacterAnimID.IS_COOLDOWN, false);

            break;
        }

        if (false == _isPlayableCharacter) return;

        _rotator = GetComponent<CharacterRotator>();
        _mainCamera = GameObject.Find("Field").transform.Find("Main Camera").gameObject;
    }

    private void Rotate(Vector2 input)
    {
        float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

        _rotator.RotateSmoothly(targetRotation);
    }

    private void Move(Vector2 input)
    {
        Vector3 moveDir = transform.forward * Mathf.Abs(input.magnitude);

        transform.position += moveDir * (Time.deltaTime * _moveSpeed);

        BlendAnimation(input.magnitude);
    }

    private void InitCoroutine()
    {
        if (null != _coroutineSetNavMeshPath)
        {
            StopCoroutine(_coroutineSetNavMeshPath);
        }

        _coroutineSetNavMeshPath = null;
    }

    private IEnumerator SetNavMeshPath()
    {
        while (true == enabled)
        {
            if (null != _targetManager.EnemyTarget)
            {
                _navMeshAgent.SetDestination(_targetManager.EnemyTarget.position);
            }
            else if (null != _targetManager.Target)
            {
                _navMeshAgent.SetDestination(_targetManager.Target.position);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(PATH_FIND_DELAY);
        }
    }

    private void ElapseCooldown(object sender, EventArgs args)
    {
        _canUseSkill = true;
    }

    private void OnDisable()
    {
        InitCoroutine();

        _animationBlend = 0f;
    }

    private void BlendAnimation(float curValue)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, curValue, Time.deltaTime * 10f);

        _animator.SetFloat(CharacterAnimID.MOVE, _animationBlend);
    }

    public void ChangeRootMotionSettings()
    {
        _animator.applyRootMotion = !_animator.applyRootMotion;

        if (false == _animator.applyRootMotion)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public override void UpdateState()
    {
        if (true == _isPlayableCharacter)
        {
            if (true == _playerInput.Attack)
            {
                _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

                return;
            }

            if (true == _playerInput.Skill && true == _canUseSkill && null != _targetManager.EnemyTarget)
            {
                _animator.SetTrigger(CharacterAnimID.USE_SKILL);

                _canUseSkill = false;

                return;
            }

            Vector2 input = new Vector2(_playerInput.Horizontal, _playerInput.Vertical);

            if (ZERO_VECTOR2 != input)
            {
                _navMeshAgent.isStopped = true;

                Rotate(input);
                Move(input);

                return;
            }

            if (false == _isAuto)
            {
                _navMeshAgent.isStopped = true;

                BlendAnimation(IDLE_ANIMATION_PARAMETER_VALUE);

                return;
            }
        }

        if (null == _targetManager.Target && null == _targetManager.EnemyTarget)
        {
            BlendAnimation(IDLE_ANIMATION_PARAMETER_VALUE);
            _navMeshAgent.ResetPath();
            InitCoroutine();

            return;
        }

        if (null == _coroutineSetNavMeshPath)
        {
            _coroutineSetNavMeshPath = StartCoroutine(SetNavMeshPath());

            return;
        }

        if (true == _canUseSkill && null != _targetManager.EnemyTarget)
        {
            _animator.SetTrigger(CharacterAnimID.USE_SKILL);

            _canUseSkill = false;

            _navMeshAgent.isStopped = true;

            return;
        }

        if (_stoppingDistance >= _navMeshAgent.remainingDistance)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

            _navMeshAgent.isStopped = true;

            return;
        }

        _navMeshAgent.isStopped = false;
        BlendAnimation(RUN_ANIMATION_PARAMETER_VALUE);
    }

    public override void ExitState()
    {
        _animator.SetFloat(CharacterAnimID.MOVE, IDLE_ANIMATION_PARAMETER_VALUE);
        _animationBlend = 0f;

        _navMeshAgent.ResetPath();

        InitCoroutine();
    }
}
