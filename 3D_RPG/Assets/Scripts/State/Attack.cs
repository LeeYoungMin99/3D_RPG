using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public Collider collider;
    protected override void Awake()
    {
        stateTag = "Attack";

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
