using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : State
{
    private DeathEventArgs _deathEvent = new DeathEventArgs();

    public event EventHandler<DeathEventArgs> EnemyDeathEvent;

    public int Experience { private get; set; } = 1;
    public int ID { private get; set; }

    protected override void Awake()
    {
        _stateTag = EStateTag.Death;

        _deathEvent.GameObject = gameObject;
        _deathEvent.ID = ID;

        CharacterStatus characterStatus = GetComponent<CharacterStatus>();

        characterStatus.OnDeathEvent -= CallSpawn;
        characterStatus.OnDeathEvent += CallSpawn;

        EnemyDeathEvent -= QuestManager.Instance.EvaluateQuestKillGoal;
        EnemyDeathEvent += QuestManager.Instance.EvaluateQuestKillGoal;

        characterStatus.Experience = Experience;

        base.Awake();
    }

    private void CallSpawn(object sender, DeathEventArgs args)
    {
        EnemyDeathEvent?.Invoke(this, _deathEvent);
    }
}
