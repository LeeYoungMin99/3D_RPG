using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStateTag
{
    Idle,
    FirstComboAttack,
    SecondComboAttack,
    ThirdComboAttack,
    Skill,
    Death = 16,
}

public enum ESlotTag
{
    InventorySlot,
    PlacementSlot,
    TagSlot,
}