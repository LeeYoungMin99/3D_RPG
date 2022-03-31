using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarSlayer : Quest
{
    public BoarSlayer()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "부탁하네...";

        farmer.CurDialogueCount = 1;

        Goals.Add(new KillGoal(0, 5));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "흉폭해진 돼지 처치";

        Description = "돼지를 5마리 처치하세요.\nAuto Button을 누르면 자동으로 찾아갈 수 있습니다.";

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new SecondFarmerTalk();
    }
}
