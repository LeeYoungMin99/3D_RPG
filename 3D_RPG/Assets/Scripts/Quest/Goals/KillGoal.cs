using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int EnemyID { get; set; }

    public KillGoal(int id, int requireAmount)
    {
        EnemyID = id;
        Completed = false;
        CurAmount = 0;
        RequiredAmount = requireAmount;

        Init();
    }

    public override void Init()
    {
        base.Init();

        QuestManager.Instance.OnEnemyDeathEvent -= EnemyDied;
        QuestManager.Instance.OnEnemyDeathEvent += EnemyDied;
    }

    private void EnemyDied(object sender, DeathEventArgs args)
    {
        if (args.ID != EnemyID) return;

        ++CurAmount;

        if (true == Evaluate())
        {
            QuestManager.Instance.OnEnemyDeathEvent -= EnemyDied;
        }
    }
}
