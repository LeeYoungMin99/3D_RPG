using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkGoal : Goal
{
    public int NPCID { get; set; }

    public TalkGoal(int id)
    {
        NPCID = id;
        Completed = false;
        CurAmount = 0;
        RequiredAmount = 1;

        Init();
    }

    public override void Init()
    {
        base.Init();

        QuestManager.Instance.TalkEvent -= TalkNPC;
        QuestManager.Instance.TalkEvent += TalkNPC;
    }

    private void TalkNPC(object sender, TalkEventArgs args)
    {
        if (args.ID != NPCID) return;

        ++CurAmount;

        if (true == Evaluate())
        {
            QuestManager.Instance.TalkEvent -= TalkNPC;
        }
    }
}
