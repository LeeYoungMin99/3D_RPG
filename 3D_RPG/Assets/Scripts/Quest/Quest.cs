using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private QuestCompleteEventArgs _questCompleteEventArgs = new QuestCompleteEventArgs();

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public string[] CharacterReward { get; set; }

    protected virtual void CheckGoals(object sender, EventArgs args)
    {
        foreach (Goal goal in Goals)
        {
            if (false == goal.Completed) return;
        }

        _questCompleteEventArgs.ExperienceReward = ExperienceReward;
        _questCompleteEventArgs.CharacterReward = CharacterReward;

        QuestManager.Instance.QuestCompleteEvent?.Invoke(this, _questCompleteEventArgs);
    }
}
