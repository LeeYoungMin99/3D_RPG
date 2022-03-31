using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFarmerTalk : Quest
{
    public FirstFarmerTalk()
    {
        Transform field = GameObject.Find("Field").transform;
        Farmer farmer = field.Find("Farmer").GetComponent<Farmer>();
        field.Find("Canvas").Find("Auto Button").GetComponent<AutoButton>().AutoMoveTarget = field.Find("Farmer");

        farmer.Dialogues[0] = "반갑네. 난 이 마을의 농부일세";
        farmer.Dialogues[1] = "보아하니 힘 좀 쓸 것 같은데 나 좀 도와주게";
        farmer.Dialogues[2] = "내가 농사를 짓던 곳에 흉포해진 돼지들이 내려왔어";
        farmer.Dialogues[3] = "저 돼지들을 처치해주게";

        farmer.CurDialogueCount = 4;

        Goals.Add(new TalkGoal(0));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "농부와 대화하기";

        Description = "농부를 찾아가세요.\nAuto Button을 누르면 자동으로 찾아갈 수 있습니다.";

        ExperienceReward = 10;

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new BoarSlayer();
    }
}
