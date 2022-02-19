using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;
    private float _moveSpeed { get; set; } = 5f;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector2 moveInput = new Vector2(_input.InputHorizontal, _input.InputVertical);

        _animator.SetFloat(PlayerAnimID.MOVE, moveInput.magnitude);

        if (_animator.GetFloat(PlayerAnimID.MOVE) != 0)
        {
            Vector3 lookForward = new Vector3(_cameraPoint.forward.x, 0f, _cameraPoint.forward.z).normalized;
            Vector3 lookRight = new Vector3(_cameraPoint.right.x, 0f, _cameraPoint.right.z).normalized;

            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            _rotator.Rotate(moveDir);

            _transform.position += moveDir * Time.deltaTime * _moveSpeed;
        }
    }
}
