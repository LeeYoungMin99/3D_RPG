using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSlayer : Quest
{
    public GhoulSlayer()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "구울은 큰 동작을 한 이후에 큰 공격을 한다더군 조심하게";

        farmer.CurDialogueCount = 1;

        Goals.Add(new KillGoal(1, 1));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "구울 처치";

        Description = "구울을 1마리 처치하세요.\nAuto Button을 누르면 자동으로 찾아갈 수 있습니다.";

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new ThirdFarmerTalk();
    }
}
