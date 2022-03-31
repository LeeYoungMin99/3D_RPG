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

        farmer.Dialogues[0] = "����";
        farmer.Dialogues[1] = "�������� �츮 �󰡿��� �⸣�� ���� �Ѹ����� �ְڳ�!";
        farmer.Dialogues[2] = "��! �׸��� �������� �������� ������ �˰� �Ƴ�";
        farmer.Dialogues[3] = "��ó�� ������ ��Ÿ���� �ֺ� �������� �������� ���̶�� �ϴ���";
        farmer.Dialogues[4] = "����� �ذ��ϱ� ���� �� ���谡���� ã�ƿ� �ּ̳�";
        farmer.Dialogues[5] = "���� ������ óġ ���� �� �ְڳ�?";

        farmer.CurDialogueCount = 6;

        Goals.Add(new TalkGoal(0));
        Goals[0].EvaluateEvent -= CheckGoals;
        Goals[0].EvaluateEvent += CheckGoals;

        QuestName = "�ٽ� ��ο� ��ȭ�ϱ�";

        Description = "��θ� ã�ư�����.\nAuto Button�� ������ �ڵ����� ã�ư� �� �ֽ��ϴ�.";

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
