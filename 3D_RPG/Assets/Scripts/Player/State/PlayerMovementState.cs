using UnityEngine;

public class PlayerMovementState : Movement
{
    private GameObject _mainCamera;
    private PlayerInput _input;
    private CharacterRotator _rotator;

    private static readonly Vector2 ZERO_VECTOR2 = Vector2.zero;
    protected override void Start()
    {
        base.Start();

        _rotator = GetComponent<CharacterRotator>();
        _input = GetComponent<PlayerInput>();
        _mainCamera = GameObject.Find("Field").transform.Find("Main Camera").gameObject;
    }

    public override void UpdateState()
    {
        if (true == _input.Attack)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACK);

            return;
        }

        Vector2 input = new Vector2(_input.Horizontal, _input.Vertical);

        if (ZERO_VECTOR2 != input)
        {
            Rotate(input);
            Move(input);

            return;
        }

        base.UpdateState();
    }

    private void Rotate(Vector2 input)
    {
        float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

        _rotator.RotateSmoothDampAngle(targetRotation);
    }

    private void Move(Vector2 input)
    {
        Vector3 moveDir = transform.forward * Mathf.Abs(input.magnitude);

        transform.position += moveDir * (Time.deltaTime * _moveSpeed);

        BlendAnimation(input.magnitude);
    }

    //protected void MoveAutomatically()
    //{
    //    Vector3 targetPosition = target.position;
    //    targetPosition.y = 0f;
    //    
    //    Vector3 myPosition = transform.position;
    //    myPosition.y = 0f;
    //    
    //    Vector3 targetDir = (targetPosition - myPosition).normalized;
    //    
    //    Vector3 cross = Vector3.Cross(transform.forward, targetDir);
    //    
    //    float dot = Vector3.Dot(transform.forward, targetDir);
    //    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
    //    
    //    if (0f > cross.y)
    //    {
    //        angle *= -1;
    //    }
    //    
    //    Move(new Vector2(1, 0));
    //    
    //    _animator.SetFloat(PlayerAnimID.MOVE, 1f);
    //    _rotator.RotateSmoothly(angle);
    //}
}
