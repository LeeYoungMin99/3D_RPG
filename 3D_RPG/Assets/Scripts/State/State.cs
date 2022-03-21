using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected EStateTag stateTag;

    protected StateMachine _stateMachine;
    protected PlayerInput _input;
    protected bool _isPlayableCharacter = false;
    protected bool _isAuto = false;

    private void SetAuto(object sender, AutoButton.AutoButtonEventArgs args)
    {
        _isAuto = args.CurAuto;
    }

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(stateTag, this);

        _input = transform.parent.GetComponent<PlayerInput>();

        if (null != _input)
        {
            _isPlayableCharacter = true;

            AutoButton autoButton = GameObject.Find("Auto Button").GetComponent<AutoButton>();
            _isAuto = autoButton.IsAuto;
            autoButton.OnClickEvent -= SetAuto;
            autoButton.OnClickEvent += SetAuto;
        }
        else
        {
            _isPlayableCharacter = false;
        }
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void LateUpdateState() { }

    public virtual void ExitState() { }
}
