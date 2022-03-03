using UnityEngine;

public class PlayerMovement : State
{
    private PlayerRotator _rotator;
    private PlayerInput _input;
    private Animator _animator;
    private Transform _cameraRoot;
    private Transform _player;

    private float _moveSpeed = 5f;

    protected override void Awake()
    {
        eStateTag = EStateTag.Movement;

        base.Awake();
    }

    private void Start()
    {
        _rotator = GetComponent<PlayerRotator>();
        _input = transform.parent.GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _cameraRoot = transform.parent.Find("Camera Root");
        _player = transform.parent;
    }

    public override void EnterState()
    {

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
        _animator.SetFloat(PlayerAnimID.MOVE, 0);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(_input.InputHorizontal, _input.InputVertical);

        _animator.SetFloat(PlayerAnimID.MOVE, Mathf.Clamp(moveInput.magnitude, 0f, 1f));

        if (0 != _animator.GetFloat(PlayerAnimID.MOVE))
        {
            Vector3 lookForward = new Vector3(_cameraRoot.forward.x, 0f, _cameraRoot.forward.z).normalized;
            Vector3 lookRight = new Vector3(_cameraRoot.right.x, 0f, _cameraRoot.right.z).normalized;
            
            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            _rotator.Rotate(new Vector2(_input.InputHorizontal, _input.InputVertical), _cameraRoot);

            _player.position += moveDir * Time.deltaTime * _moveSpeed;
        }
    }
}
