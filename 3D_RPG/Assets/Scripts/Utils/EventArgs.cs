using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SlotClickEventArgs : EventArgs
{
    public CharacterData CharacterData;
}

public class AutoButtonEventArgs : EventArgs
{
    public bool CurAuto;
    public Transform AutoMoveTarget;
}

public class DeathEventArgs : EventArgs
{
    public GameObject GameObject;
    public int ID;
    public int Experience;
}

public class DataChangeEventArgs : EventArgs
{
    public string Name;
    public int Level;
    public float ATK;
    public float NormalizedCurHP;
    public float CurHP;
    public float MaxHP;
    public float CurExperience;
}

public class SkillEventArgs : EventArgs
{
    public CharacterData CharacterData;
    public float CooldownTime;
}

public class QuestChangeEventArgs : EventArgs
{
    public string QuestName;
    public string QuestDescription;
}

public class TalkEventArgs : EventArgs
{
    public int ID;
}

public class QuestCompleteEventArgs : EventArgs
{
    public int ExperienceReward;
    public string[] CharacterReward;
}