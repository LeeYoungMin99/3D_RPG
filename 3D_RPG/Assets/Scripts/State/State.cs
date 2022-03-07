using UnityEngine;

public class State : MonoBehaviour
{
    protected StateMachine _stateMachine;
    protected string stateTag;

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(stateTag, this);
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
