using UnityEngine;

public class PlayerMovementState : State
{
    [SerializeField] private float _moveSpeed = 5f;

    private PlayerRotator _rotator;
    private Transform _player;
    private PlayerInput _input;
    private Animator _animator;
    private Transform _cameraRoot;

    private float _moveInput = 0f;

    protected override void Awake()
    {
        stateTag = EStateTag.Movement;

        base.Awake();
    }

    private void Start()
    {
        _rotator = GetComponent<PlayerRotator>();
        _player = transform.parent;
        _input = _player.GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _cameraRoot = transform.parent.Find("Camera Root");
    }

    public override void UpdateState()
    {
        if (_input.InputAttack)
        {
            _animator.SetTrigger(PlayerAnimID.IS_ATTACK);

            return;
        }

        Move();
    }

    public override void ExitState()
    {
        _moveInput = 0;
        _animator.SetFloat(PlayerAnimID.MOVE, _moveInput);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(_input.InputHorizontal, _input.InputVertical);

        _moveInput = Mathf.Clamp(moveInput.magnitude, 0f, 1f);

        _animator.SetFloat(PlayerAnimID.MOVE, _moveInput);

        if (0 != _moveInput)
        {
            Vector3 lookForward = new Vector3(_cameraRoot.forward.x, 0f, _cameraRoot.forward.z).normalized;
            Vector3 lookRight = new Vector3(_cameraRoot.right.x, 0f, _cameraRoot.right.z).normalized;

            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            _rotator.Rotate(moveInput, _cameraRoot);

            _player.position += moveDir * (Time.deltaTime * _moveSpeed);

            StartCoroutine(InitializeLocalPositionAtEndOfFrame());
        }
    }
}
