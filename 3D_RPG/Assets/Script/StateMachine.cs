using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<EStateTag, IState> _states = new Dictionary<EStateTag, IState>();

    private IState _curState;

    private void Start()
    {
        AddState(EStateTag.Movement, GetComponent<PlayerMovement>());
        AddState(EStateTag.Attack, GetComponent<PlayerAttack>());
    }

    private void Update()
    {
        _curState?.Update();
    }

    public void AddState(EStateTag tag, IState state)
    {
        _states[tag] = state;
    }

    public void ChangeState(int tag)
    {
        if (_curState == _states[(EStateTag)tag])
            return;

        Debug.Log($"{(EStateTag)tag}로 상태가 변경됨");

        _curState?.Exit();

        _curState = _states[(EStateTag)tag];

        _curState.Enter();
    }
}
