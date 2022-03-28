using System;
using UnityEngine;

public class CharacterData
{
    private GameObject _characterPawn;
    private CharacterStatus _characterStatus;
    private AttackState _skill;

    public Sprite InventorySlotSprite { get; private set; }
    public Sprite PlacementSlotSprite { get; private set; }
    public Sprite TagSlotSprite { get; private set; }

    public bool PawnActive { get { return _characterPawn.activeSelf; } }
    public Vector3 PawnPosition { get { return _characterPawn.transform.position; } }
    public CharacterStatus CharacterStatus { get { return _characterStatus; } }
    public AttackState Skill { get { return _skill; } }

    public CharacterData(string name, GameObject pawn)
    {
        _characterPawn = pawn;
        _characterStatus = pawn.GetComponent<CharacterStatus>();

        AttackState[] attackStates = pawn.GetComponents<AttackState>();

        foreach (AttackState attackState in attackStates)
        {
            if (false == attackState.IsSkill) continue;
            
            _skill = attackState;

            break;
        }

        InventorySlotSprite = Resources.Load<Sprite>($"Images/Inventory Slot/{name}");
        PlacementSlotSprite = Resources.Load<Sprite>($"Images/Placement Slot/{name}");
        TagSlotSprite = Resources.Load<Sprite>($"Images/Tag Slot/{name}");
    }

    public void SetCharacterPawnActive(bool b)
    {
        _characterPawn.SetActive(b);
    }

    public void SetPawnPosition(Vector3 position)
    {
        _characterPawn.transform.position = position;
    }

    public bool CheckDeath()
    {
        if (0 >= _characterStatus.CurHP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
