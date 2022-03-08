using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    [SerializeField] protected float _delay = 0.2f;

    protected override void Awake()
    {
        stateTag = EStateTag.Attack;

        base.Awake();
    }

    public abstract IEnumerator GiveDamage();
}
