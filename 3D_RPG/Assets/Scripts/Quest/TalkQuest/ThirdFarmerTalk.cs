using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdFarmerTalk : Quest
{
    public ThirdFarmerTalk()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "정말 고맙네...";

        farmer.CurDialogueCount = 1;

        Goals.Add(new TalkGoal(0));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "마지막으로 농부와 대화하기";

        Description = "농부를 찾아가세요.\nAuto Button을 누르면 자동으로 찾아갈 수 있습니다.";

        ExperienceReward = 10;

        CharacterReward = new string[1];
        CharacterReward[0] = "Ghoul";

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        QuestManager.Instance.CurQuest = null;
    }
}
