using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected EStateTag _stateTag;

    protected PlayerInput _playerInput;
    protected TargetManager _targetManager;
    protected bool _isPlayableCharacter = false;
    protected bool _isAuto = false;

    private void SetAuto(object sender, AutoButtonEventArgs args)
    {
        _isAuto = args.CurAuto;
        _targetManager.Target = args.AutoMoveTarget;
    }

    protected virtual void Awake()
    {
        GetComponent<StateMachine>().AddState(_stateTag, this);

        _playerInput = transform.parent.GetComponent<PlayerInput>();

        if (null != _playerInput)
        {
            _isPlayableCharacter = true;

            AutoButton autoButton = GameObject.Find("Auto Button").GetComponent<AutoButton>();
            autoButton.OnAutoButtonClickEvent -= SetAuto;
            autoButton.OnAutoButtonClickEvent += SetAuto;

            _isAuto = autoButton.IsAuto;

            _targetManager = GetComponent<PlayerTargetManager>();
        }
        else
        {
            _isPlayableCharacter = false;

            _targetManager = GetComponent<TargetManager>();
        }
    }

    public virtual void EnterState() { }

    public virtual void FixedUpdateState() { }

    public virtual void UpdateState() { }

    public virtual void LateUpdateState() { }

    public virtual void ExitState() { }
}
