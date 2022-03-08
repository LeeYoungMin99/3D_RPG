using System.Collections;
using UnityEngine;

public class State : MonoBehaviour
{
    protected StateMachine _stateMachine;
    protected EStateTag stateTag;

    private static readonly Vector3 VEC_ZERO = Vector3.zero;

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(stateTag, this);
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }

    protected IEnumerator InitializeLocalPositionAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        transform.localPosition = VEC_ZERO;
    }
}
