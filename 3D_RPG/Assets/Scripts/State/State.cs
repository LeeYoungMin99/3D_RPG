using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected EStateTag stateTag;

    protected StateMachine _stateMachine;

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(stateTag, this);

    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void LateUpdateState() { }

    public virtual void ExitState() { }
}
