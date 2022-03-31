using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarSlayer : Quest
{
    public BoarSlayer()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "��Ź�ϳ�...";

        farmer.CurDialogueCount = 1;

        Goals.Add(new KillGoal(0, 5));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "�������� ���� óġ";

        Description = "������ 5���� óġ�ϼ���.\nAuto Button�� ������ �ڵ����� ã�ư� �� �ֽ��ϴ�.";

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new SecondFarmerTalk();
    }
}
