using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : State
{
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected float _autoAttackDistance = 2f;

    protected CharacterRotator _rotator;
    protected Animator _animator;
    protected TargetManager _targetManager;
    protected NavMeshAgent _navMeshAgent;

    protected Coroutine _coroutineSetNavmeshPathToEnemyTargetPosition;

    protected static readonly float PATH_FIND_DELAY = 0.25f;
    protected static readonly float RUN_ANIMATION_PARAMETER_VALUE = 1f;
    protected static readonly float IDLE_ANIMATION_PARAMETER_VALUE = 0f;
    protected static readonly Vector2 INPUT_MOVE_FORWARD = new Vector2(0, 1);

    private void OnDisable()
    {
        InitCoroutine();
    }

    protected virtual void Start()
    {
        _rotator = GetComponent<CharacterRotator>();
        _animator = GetComponent<Animator>();
        _targetManager = GetComponent<TargetManager>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void Move(Transform transform, Vector2 moveInput, float animationParameterValue)
    {
        _animator.SetFloat(PlayerAnimID.MOVE, animationParameterValue);

        Vector3 lookForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 lookRight = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

        Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

        float angle = Mathf.Atan2(moveInput.x, moveInput.y);

        _rotator.Rotate(angle, transform);

        transform.position += moveDir * (Time.deltaTime * _moveSpeed);

        StartCoroutine(InitializeLocalPositionAtEndOfFrame());
    }

    protected bool CheckNavMeshStop(float stoppingDistance)
    {
        if (stoppingDistance > _navMeshAgent.remainingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void MoveToNavMeshPath()
    {
        _navMeshAgent.isStopped = false;

        Move(transform, INPUT_MOVE_FORWARD, RUN_ANIMATION_PARAMETER_VALUE);
    }

    protected void StopNavMesh()
    {
        _navMeshAgent.isStopped = true;

        _animator.SetFloat(PlayerAnimID.MOVE, IDLE_ANIMATION_PARAMETER_VALUE);

        _navMeshAgent.ResetPath();
    }

    protected IEnumerator SetNavmeshPathToEnemyTargetPosition()
    {
        while (true == enabled)
        {
            if (null == _targetManager.EnemyTarget || null != _targetManager.Target)
            {
                StopNavMesh();

                InitCoroutine();
            }
            else
            {
                _navMeshAgent.SetDestination(_targetManager.EnemyTarget.position);
            }

            yield return new WaitForSeconds(PATH_FIND_DELAY);
        }
    }

    protected void InitCoroutine()
    {
        if (null != _coroutineSetNavmeshPathToEnemyTargetPosition)
        {
            StopCoroutine(_coroutineSetNavmeshPathToEnemyTargetPosition);

            _coroutineSetNavmeshPathToEnemyTargetPosition = null;
        }
    }

    public override void UpdateState()
    {
        if (null != _targetManager.EnemyTarget)
        {
            if (null == _coroutineSetNavmeshPathToEnemyTargetPosition)
            {
                _coroutineSetNavmeshPathToEnemyTargetPosition = StartCoroutine(SetNavmeshPathToEnemyTargetPosition());

                _navMeshAgent.isStopped = false;

                return;
            }

            MoveToNavMeshPath();

            if (true == CheckNavMeshStop(_autoAttackDistance))
            {
                _animator.SetTrigger(PlayerAnimID.IS_ATTACK);

                StopNavMesh();
            }
        }
    }

    public override void ExitState()
    {
        StopNavMesh();

        InitCoroutine();
    }
}
