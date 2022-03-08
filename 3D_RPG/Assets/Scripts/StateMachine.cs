using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private static EnumComparer _enumComparer = new EnumComparer();
    private Dictionary<EStateTag, State> _states = new Dictionary<EStateTag, State>(_enumComparer);

    private State _curState;

    private void Start()
    {
        _curState = _states[EStateTag.Movement];
    }

    private void Update()
    {
        _curState.UpdateState();
    }

    public void AddState(EStateTag tag, State state)
    {
        _states[tag] = state;
    }

    public void ChangeState(int tag)
    {
        if (_curState == _states[(EStateTag)tag])
            return;

        Debug.Log($"{tag}로 상태가 변경됨");

        _curState.ExitState();

        _curState = _states[(EStateTag)tag];

        _curState.EnterState();
    }
}
