using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<EStateTag, State> _states = new Dictionary<EStateTag, State>(new EnumComparer());

    private State _curState;

    private void Start()
    {
        _curState = _states[EStateTag.Idle];
    }

    private void FixedUpdate()
    {
        _curState.FixedUpdateState();
    }

    private void Update()
    {
        _curState.UpdateState();
    }

    private void LateUpdate()
    {
        _curState.LateUpdateState();
    }

    public void AddState(EStateTag tag, State state)
    {
        _states[tag] = state;
    }

    public void ChangeState(int tag)
    {
        if (_curState == _states[(EStateTag)tag]) return;

        _curState.ExitState();

        _curState = _states[(EStateTag)tag];

        _curState.EnterState();
    }
}
