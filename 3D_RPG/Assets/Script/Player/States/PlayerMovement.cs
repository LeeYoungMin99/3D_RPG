using UnityEngine;

public class PlayerMovement : MonoBehaviour, IState
{
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Animator _animator;
    [SerializeField] private StateMachine _stateMachine;

    private PlayerInput _input;
    private Transform _cameraPoint;
    private Transform _managerTransform;

    private float _moveSpeed { get; set; } = 5f;

    private void Start()
    {
        _cameraPoint = transform.parent.Find("Camera Point");
        _managerTransform = transform.parent;
        _input = transform.parent.gameObject.GetComponent<PlayerInput>();
    }

    public void Enter() { }

    void IState.Update()
    {
        if (_input.InputAttack)
        {
            _stateMachine.ChangeState((int)EStateTag.Attack);
            return;
        }

        Move();
    }

    public void Exit()
    {
        _animator.SetFloat(PlayerAnimID.MOVE, 0);
        _animator.applyRootMotion = false;
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(_input.InputHorizontal, _input.InputVertical);

        _animator.SetFloat(PlayerAnimID.MOVE, Mathf.Clamp(moveInput.magnitude, 0f, 1f));

        if (_animator.GetFloat(PlayerAnimID.MOVE) != 0)
        {
            _animator.applyRootMotion = false;

            Vector3 lookForward = new Vector3(_cameraPoint.forward.x, 0f, _cameraPoint.forward.z).normalized;
            Vector3 lookRight = new Vector3(_cameraPoint.right.x, 0f, _cameraPoint.right.z).normalized;

            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            _rotator.Rotate(new Vector2(_input.InputHorizontal, _input.InputVertical), _cameraPoint);

            _managerTransform.position += moveDir * Time.deltaTime * _moveSpeed;
        }
        else
        {
            _animator.applyRootMotion = true;
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

}
