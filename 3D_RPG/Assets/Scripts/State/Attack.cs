using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    protected override void Awake()
    {
        stateTag = EStateTag.Attack;

        base.Awake();
    }
}
