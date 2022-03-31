using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSlayer : Quest
{
    public GhoulSlayer()
    {
        Transform field = GameObject.Find("Field").transform;
        Farmer farmer = field.Find("Farmer").GetComponent<Farmer>();
        field.Find("Canvas").Find("Auto Button").GetComponent<AutoButton>().AutoMoveTarget = field.Find("Spawners").Find("Ghoul Spawner");

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
