using UnityEngine;

public class State : MonoBehaviour
{
    protected StateMachine _stateMachine;
    protected EStateTag eStateTag;

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(eStateTag, this);
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
