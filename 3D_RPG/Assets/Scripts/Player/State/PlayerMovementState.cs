using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementState : State
{
    [SerializeField] private float _moveSpeed = 5f;

    private PlayerRotator _rotator;
    private Animator _animator;
    private Transform _player;
    private PlayerInput _input;
    private TargetManager _targetManager;
    private NavMeshAgent _navMeshAgent;
    private Transform _cameraRoot;
    private CameraRotator _cameraRotator;

    private static readonly float PATH_FIND_DELAY = 0.25f;
    private static readonly float STOPPING_DISTANCE = 3f;
    private static readonly float RUN_ANIMATION_PARAMETER_VALUE = 1f;
    private static readonly float IDLE_ANIMATION_PARAMETER_VALUE = 0f;
    private static readonly Vector2 INPUT_MOVE_FORWARD = new Vector2(0, 1);

    private float _moveAnimationParameterValue = 0f;
    private Vector2 _moveInput;
    private Coroutine _coroutineSetNavmeshPathToEnemyTargetPosition;

    public bool bIsAuto = true;

    private void Start()
    {
        _rotator = GetComponent<PlayerRotator>();
        _animator = GetComponent<Animator>();
        _player = transform.parent;
        _input = _player.GetComponent<PlayerInput>();
        _targetManager = _player.GetComponent<TargetManager>();
        _navMeshAgent = _player.GetComponent<NavMeshAgent>();
        _cameraRoot = _player.Find("Camera Root");
        _cameraRotator = _cameraRoot.GetComponent<CameraRotator>();
    }

    private void OnDisable()
    {
        if (null != _coroutineSetNavmeshPathToEnemyTargetPosition)
        {
            StopCoroutine(_coroutineSetNavmeshPathToEnemyTargetPosition);
        }
    }

    private bool CheckMoveInput()
    {
        _moveInput = new Vector2(_input.InputHorizontal, _input.InputVertical);

        float inputValue = Mathf.Clamp(_moveInput.magnitude, 0f, 1f);

        _moveAnimationParameterValue = inputValue;

        if (0 == inputValue)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Move(Vector2 moveInput, float animationParameterValue)
    {
        _animator.SetFloat(PlayerAnimID.MOVE, animationParameterValue);

        Vector3 lookForward = new Vector3(_cameraRoot.forward.x, 0f, _cameraRoot.forward.z).normalized;
        Vector3 lookRight = new Vector3(_cameraRoot.right.x, 0f, _cameraRoot.right.z).normalized;

        Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

        float angle = Mathf.Atan2(moveInput.x, moveInput.y);

        _rotator.Rotate(angle, _cameraRoot);

        _player.position += moveDir * (Time.deltaTime * _moveSpeed);

        StartCoroutine(InitializeLocalPositionAtEndOfFrame());
    }

    private void MoveToNavMeshPath()
    {
        if (0 != _moveAnimationParameterValue)
        {
            _navMeshAgent.isStopped = true;

            return;
        }

        if (STOPPING_DISTANCE > _navMeshAgent.remainingDistance)
        {
            _navMeshAgent.isStopped = true;

            _animator.SetFloat(PlayerAnimID.MOVE, IDLE_ANIMATION_PARAMETER_VALUE);

            _navMeshAgent.ResetPath();

            _targetManager.Target = null;

            return;
        }
        else
        {
            _navMeshAgent.isStopped = false;

            Move(INPUT_MOVE_FORWARD, RUN_ANIMATION_PARAMETER_VALUE);

            _cameraRotator.RotateAutoMoveCameraAngle();
        }
    }

    private IEnumerator SetNavmeshPathToEnemyTargetPosition()
    {
        while (true == enabled)
        {
            _navMeshAgent.SetDestination(_targetManager.EnemyTarget.position);

            yield return new WaitForSeconds(PATH_FIND_DELAY);

            _coroutineSetNavmeshPathToEnemyTargetPosition = StartCoroutine(SetNavmeshPathToEnemyTargetPosition());
        }
    }

    public override void UpdateState()
    {
        if (true == CheckMoveInput())
        {
            Move(_moveInput, _moveAnimationParameterValue);
        }

        if (false == bIsAuto) { return; }

        if (null != _targetManager.Target)
        {
            if (false == _navMeshAgent.hasPath || null != _coroutineSetNavmeshPathToEnemyTargetPosition)
            {
                _navMeshAgent.SetDestination(_targetManager.Target.position);

                _navMeshAgent.isStopped = false;

                if (null != _coroutineSetNavmeshPathToEnemyTargetPosition)
                {
                    StopCoroutine(_coroutineSetNavmeshPathToEnemyTargetPosition);
                }

                return;
            }

            MoveToNavMeshPath();
        }
        else if (null != _targetManager.EnemyTarget)
        {
            if (null == _coroutineSetNavmeshPathToEnemyTargetPosition)
            {
                _coroutineSetNavmeshPathToEnemyTargetPosition = StartCoroutine(SetNavmeshPathToEnemyTargetPosition());

                _navMeshAgent.isStopped = false;

                return;
            }

            MoveToNavMeshPath();
        }
    }

    public override void ExitState()
    {
        _moveAnimationParameterValue = 0;

        _animator.SetFloat(PlayerAnimID.MOVE, _moveAnimationParameterValue);

        _navMeshAgent.isStopped = true;

        _navMeshAgent.ResetPath();

        if (null != _coroutineSetNavmeshPathToEnemyTargetPosition)
        {
            StopCoroutine(_coroutineSetNavmeshPathToEnemyTargetPosition);
        }
    }
}
