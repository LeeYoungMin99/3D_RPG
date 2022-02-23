using UnityEngine;

public class PlayerMovement : MonoBehaviour, IState
{
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private Transform _managerTransform;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private StateMachine _stateMachine;

    private float _moveSpeed { get; set; } = 5f;
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
        //_animator.applyRootMotion = true;
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

            _rotator.Rotate(moveDir);

            _managerTransform.position += moveDir * Time.deltaTime * _moveSpeed;
        }
        else
        {
            _animator.applyRootMotion = true;
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

}
