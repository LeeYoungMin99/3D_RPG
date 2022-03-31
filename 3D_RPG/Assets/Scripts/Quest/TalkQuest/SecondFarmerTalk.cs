using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFarmerTalk : Quest
{
    public SecondFarmerTalk()
    {
        Transform field = GameObject.Find("Field").transform;
        Farmer farmer = field.Find("Farmer").GetComponent<Farmer>();
        field.Find("Canvas").Find("Auto Button").GetComponent<AutoButton>().AutoMoveTarget = field.Find("Farmer");

        farmer.Dialogues[0] = "고맙네";
        farmer.Dialogues[1] = "보답으로 우리 농가에서 기르던 돼지 한마리를 주겠네!";
        farmer.Dialogues[2] = "아! 그리고 돼지들이 흉포해진 이유를 알게 됐네";
        farmer.Dialogues[3] = "근처에 구울이 나타나며 주변 동물들이 흉포해진 것이라고 하더군";
        farmer.Dialogues[4] = "사건을 해결하기 위해 두 모험가분이 찾아와 주셨네";
        farmer.Dialogues[5] = "같이 구울을 처치 해줄 수 있겠나?";

        farmer.CurDialogueCount = 6;

        Goals.Add(new TalkGoal(0));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "다시 농부와 대화하기";

        Description = "농부를 찾아가세요.\nAuto Button을 누르면 자동으로 찾아갈 수 있습니다.";

        ExperienceReward = 10;

        CharacterReward = new string[3];
        CharacterReward[0] = "Archer";
        CharacterReward[1] = "Magician";
        CharacterReward[2] = "Boar";

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new GhoulSlayer();
    }
}
