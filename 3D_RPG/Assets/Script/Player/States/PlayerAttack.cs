using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IState
{
    [SerializeField] Animator _animator;

    public void Enter()
    {
        _animator.SetTrigger(PlayerAnimID.IS_ATTACK);
    }

    void IState.Update()
    {
    }

    public void Exit() { }
}
