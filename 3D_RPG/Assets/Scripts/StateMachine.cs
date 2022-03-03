using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<EStateTag, State> _states = new Dictionary<EStateTag, State>();

    private State _curState;

    private void Start()
    {
        ChangeState((int)EStateTag.Movement);
    }

    private void Update()
    {
        _curState.UpdateState();
    }

    private void LateUpdate()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void AddState(EStateTag tag, State state)
    {
        _states[tag] = state;
    }

    public void ChangeState(int tag)
    {
        if (_curState == _states[(EStateTag)tag])
            return;

        Debug.Log($"{(EStateTag)tag}로 상태가 변경됨");

        _curState?.ExitState();

        _curState = _states[(EStateTag)tag];

        _curState.EnterState();
    }
}
