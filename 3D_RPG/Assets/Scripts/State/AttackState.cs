using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    protected override void Awake()
    {
        stateTag = EStateTag.Attack;

        base.Awake();
    }

    public abstract IEnumerator GiveDamage();
}
