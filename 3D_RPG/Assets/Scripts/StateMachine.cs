using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<string, State> _states = new Dictionary<string, State>();

    private State _curState;

    private void Start()
    {
        _curState = _states["Movement"];
    }

    private void Update()
    {
        _curState.UpdateState();
    }

    private void LateUpdate()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void AddState(string tag, State state)
    {
        _states[tag] = state;
    }

    public void ChangeState(string tag)
    {
        if (_curState == _states[tag])
            return;

        Debug.Log($"{tag}로 상태가 변경됨");

        _curState.ExitState();

        _curState = _states[tag];

        _curState.EnterState();
    }
}
