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
            _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

            return;
        }

        if (true == _input.Skill && true == _canUseSkill)
        {
            _animator.SetTrigger(CharacterAnimID.USE_SKILL);

            return;
        }

        Vector2 input = new Vector2(_input.Horizontal, _input.Vertical);

        if (ZERO_VECTOR2 != input)
        {
            _navMeshAgent.isStopped = true;

            Rotate(input);
            Move(input);
        }
        else
        {
            _navMeshAgent.isStopped = false;
            base.UpdateState();
        }
    }

    private void Rotate(Vector2 input)
    {
        float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

        _rotator.RotateSmoothly(targetRotation);
    }

    private void Move(Vector2 input)
    {
        Vector3 moveDir = transform.forward * Mathf.Abs(input.magnitude);

        transform.position += moveDir * (Time.deltaTime * _moveSpeed);

        BlendAnimation(input.magnitude);
    }
}
