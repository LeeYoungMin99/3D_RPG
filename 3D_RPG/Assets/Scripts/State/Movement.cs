using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : State
{
    [SerializeField] protected float _moveSpeed = 5f;

    private NavMeshAgent _navMeshAgent;
    private Coroutine _coroutineSetNavMeshPath;
    private float _animationBlend = 0f;
    private float _stoppingDistance;

    protected TargetManager _targetManager;
    protected Animator _animator;

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
        while (true == enabled && null != _targetManager.Target)
        {
            _navMeshAgent.SetDestination(_targetManager.Target.position);

            yield return new WaitForSeconds(PATH_FIND_DELAY);
        }
    }

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _targetManager = GetComponent<TargetManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _stoppingDistance = _navMeshAgent.stoppingDistance;
    }

    protected void OnDisable()
    {
        InitCoroutine();
    }

    public override void UpdateState()
    {
        if (null == _targetManager.Target)
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

        if (_stoppingDistance >= _navMeshAgent.remainingDistance)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACK);

            return;
        }

        BlendAnimation(RUN_ANIMATION_PARAMETER_VALUE);
    }

    public override void ExitState()
    {
        _navMeshAgent.ResetPath();

        InitCoroutine();
    }

    protected void BlendAnimation(float curValue)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, curValue, Time.deltaTime * 10f);

        _animator.SetFloat(CharacterAnimID.MOVE, _animationBlend);
    }
}
