using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSectorFormAttack : SectorFormAttackState
{
    private PlayerInput _input;

    protected override void Start()
    {
        base.Start();

        _input = GetComponent<PlayerInput>();
    }

    public override void UpdateState()
    {
        if (false == CheckComboPossible()) return;

        if (true == _input.Attack)
        {
            _animator.SetTrigger(CharacterAnimID.IS_ATTACKING);

            return;
        }

        base.UpdateState();
    }
}
