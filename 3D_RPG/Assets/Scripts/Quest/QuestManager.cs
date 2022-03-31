using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    public static readonly QuestManager Instance = new QuestManager();

    private Quest _curQuest;
    private QuestChangeEventArgs _questDataChangeEventArgs = new QuestChangeEventArgs();

    public EventHandler<QuestChangeEventArgs> OnQuestChangeEvent;
    public EventHandler<DeathEventArgs> OnEnemyDeathEvent;
    public EventHandler<TalkEventArgs> TalkEvent;
    public EventHandler<QuestCompleteEventArgs> QuestCompleteEvent;

    public Quest CurQuest
    {
        get { return _curQuest; }
        set
        {
            _curQuest = value;

            if (null == _curQuest)
            {
                _questDataChangeEventArgs.QuestName = "";
                _questDataChangeEventArgs.QuestDescription = "";
            }
            else
            {
                _questDataChangeEventArgs.QuestName = _curQuest.QuestName;
                _questDataChangeEventArgs.QuestDescription = _curQuest.Description;
            }

            OnQuestChangeEvent?.Invoke(this, _questDataChangeEventArgs);
        }
    }

    public void EvaluateQuestKillGoal(object sender, DeathEventArgs args)
    {
        OnEnemyDeathEvent?.Invoke(this, args);
    }

    public void EvaluateQuestTalkGoal(object sender, TalkEventArgs args)
    {
        TalkEvent?.Invoke(this, args);
    }
}
