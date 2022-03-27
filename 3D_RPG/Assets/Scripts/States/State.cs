using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected EStateTag stateTag;

    protected StateMachine _stateMachine;
    protected PlayerInput _playerInput;
    protected TargetManager _targetManager;
    protected bool _isPlayableCharacter = false;
    protected bool _isAuto = false;

    private void SetAuto(object sender, AutoButtonEventArgs args)
    {
        _isAuto = args.CurAuto;
    }

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(stateTag, this);

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

    public virtual void UpdateState() { }

    public virtual void LateUpdateState() { }

    public virtual void ExitState() { }
}
