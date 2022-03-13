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

    protected override void MoveToNavMeshPath()
    {
        base.MoveToNavMeshPath();

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
