using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementState : Movement
{
    private PlayerInput _input;
    private Transform _cameraRoot;
    private CameraRotator _cameraRotator;
    private Transform _player;

    private float _moveAnimationParameterValue = 0f;
    private Vector2 _moveInput;

    private static readonly float STOPPING_DISTANCE = 3f;

    protected override void Start()
    {
        _rotator = GetComponent<CharacterRotator>();
        _animator = GetComponent<Animator>();
        _player = transform.parent;
        _input = _player.GetComponent<PlayerInput>();
        _targetManager = _player.GetComponent<TargetManager>();
        _navMeshAgent = _player.GetComponent<NavMeshAgent>();
        _cameraRoot = _player.Find("Camera Root");
        _cameraRotator = _cameraRoot.GetComponent<CameraRotator>();
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

    protected override void Move(Transform transform, Vector2 moveInput, float animationParameterValue)
    {
        _animator.SetFloat(PlayerAnimID.MOVE, animationParameterValue);

        Vector3 lookForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 lookRight = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

        Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

        float angle = Mathf.Atan2(moveInput.x, moveInput.y);

        _rotator.Rotate(angle, transform);

        _player.position += moveDir * (Time.deltaTime * _moveSpeed);

        StartCoroutine(InitializeLocalPositionAtEndOfFrame());
    }

    protected override void MoveToNavMeshPath()
    {
        _navMeshAgent.isStopped = false;

        Move(_cameraRoot, INPUT_MOVE_FORWARD, RUN_ANIMATION_PARAMETER_VALUE);

        _cameraRotator.RotateCameraAngleForAutoMove();
    }

    public override void UpdateState()
    {
        if (true == _input.InputAttack)
        {
            _animator.SetTrigger(PlayerAnimID.IS_ATTACK);

            return;
        }

        if (true == CheckMoveInput())
        {
            Move(_cameraRoot, _moveInput, _moveAnimationParameterValue);
        }
        else if (null != _targetManager.Target)
        {
            if (false == _navMeshAgent.hasPath || null != _coroutineSetNavmeshPathToEnemyTargetPosition)
            {
                _navMeshAgent.SetDestination(_targetManager.Target.position);

                _navMeshAgent.isStopped = false;

                return;
            }

            MoveToNavMeshPath();

            if (true == CheckNavMeshStop(STOPPING_DISTANCE))
            {
                _targetManager.Target = null;

                StopNavMesh();
            }
        }
        else
        {
            base.UpdateState();
        }
    }
}
