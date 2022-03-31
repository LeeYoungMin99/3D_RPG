using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSlayer : Quest
{
    public GhoulSlayer()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "������ ū ������ �� ���Ŀ� ū ������ �Ѵٴ��� �����ϰ�";

        farmer.CurDialogueCount = 1;

        Goals.Add(new KillGoal(1, 1));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "���� óġ";

        Description = "������ 1���� óġ�ϼ���.\nAuto Button�� ������ �ڵ����� ã�ư� �� �ֽ��ϴ�.";

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new ThirdFarmerTalk();
    }
}
