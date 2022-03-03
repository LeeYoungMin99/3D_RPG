using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    protected override void Awake()
    {
        eStateTag = EStateTag.Attack;

        base.Awake();
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }
}
