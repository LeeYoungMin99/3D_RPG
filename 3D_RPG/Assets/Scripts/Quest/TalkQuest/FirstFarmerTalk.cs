using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFarmerTalk : Quest
{
    public FirstFarmerTalk()
    {
        Farmer farmer = GameObject.Find("Field").transform.Find("Farmer").GetComponent<Farmer>();

        farmer.Dialogues[0] = "�ݰ���. �� �� ������ ����ϼ�";
        farmer.Dialogues[1] = "�����ϴ� �� �� �� �� ������ �� �� �����ְ�";
        farmer.Dialogues[2] = "���� ��縦 ���� ���� �������� �������� �����Ծ�";
        farmer.Dialogues[3] = "�� �������� óġ���ְ�";

        farmer.CurDialogueCount = 4;

        Goals.Add(new TalkGoal(0));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "��ο� ��ȭ�ϱ�";

        Description = "��θ� ã�ư�����.\nAuto Button�� ������ �ڵ����� ã�ư� �� �ֽ��ϴ�.";

        ExperienceReward = 10;

        QuestManager.Instance.CurQuest = this;
    }

    protected override void CheckGoals(object sender, EventArgs args)
    {
        base.CheckGoals(sender, args);

        new BoarSlayer();
    }
}
