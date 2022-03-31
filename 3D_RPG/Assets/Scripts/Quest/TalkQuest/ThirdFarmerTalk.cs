using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdFarmerTalk : Quest
{
    public ThirdFarmerTalk()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "���� ����...";

        farmer.CurDialogueCount = 1;

        Goals.Add(new TalkGoal(0));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "���������� ��ο� ��ȭ�ϱ�";

        Description = "��θ� ã�ư�����.\nAuto Button�� ������ �ڵ����� ã�ư� �� �ֽ��ϴ�.";

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
