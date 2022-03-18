using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : State
{
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected bool _hasSkill = true;

    private Coroutine _coroutineSetNavMeshPath;
    private float _animationBlend = 0f;
    private float _stoppingDistance;

    protected NavMeshAgent _navMeshAgent;
    protected TargetManager _targetManager;
    protected Animator _animator;
    protected bool _canUseSkill = true;

    protected const float PATH_FIND_DELAY = 0.25f;
    protected const float IDLE_ANIMATION_PARAMETER_VALUE = 0f;
    protected const float RUN_ANIMATION_PARAMETER_VALUE = 1f;

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
            if (null != _targetManager.Target)
            {
                _navMeshAgent.SetDestination(_targetManager.Target.position);
            }
            else if (null != _targetManager.EnemyTarget)
            {
                _navMeshAgent.SetDestination(_targetManager.EnemyTarget.position);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(PATH_FIND_DELAY);
        }
    }

    public void ElapseCooldown(object sender, EventArgs args)
    {
        _canUseSkill = true;
    }

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _targetManager = GetComponent<TargetManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _stoppingDistance = _navMeshAgent.stoppingDistance;

        if (true == _hasSkill)
        {
            AttackState[] attackStates = GetComponents<AttackState>();

            foreach (AttackState attackState in attackStates)
            {
                if (true == attackState.IsSkill)
                {
                    attackState.OnCooldownElapsed -= ElapseCooldown;
                    attackState.OnCooldownElapsed += ElapseCooldown;

                    _animator.SetBool(CharacterAnimID.IS_COOLDOWN, false);
                    break;
                }
            }
        }
        else
        {
            _canUseSkill = false;
            _animator.SetBool(CharacterAnimID.IS_COOLDOWN, true);
        }
    }

    protected void OnDisable()
    {
        InitCoroutine();
    }

    protected void BlendAnimation(float curValue)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, curValue, Time.deltaTime * 10f);

        _animator.SetFloat(CharacterAnimID.MOVE, _animationBlend);
    }

    public override void UpdateState()
    {
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

            return;
        }

        if (_stoppingDistance >= _navMeshAgent.remainingDistance)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

            return;
        }

        BlendAnimation(RUN_ANIMATION_PARAMETER_VALUE);
    }

    public override void ExitState()
    {
        _animator.SetFloat(CharacterAnimID.MOVE, IDLE_ANIMATION_PARAMETER_VALUE);

        _navMeshAgent.ResetPath();

        InitCoroutine();
    }
}
