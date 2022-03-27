using System;
using UnityEngine;

public class CharacterData
{
    private GameObject _characterPawn;
    private CharacterStatus _characterStatus;

    public string Name { get; private set; }
    public Sprite InventorySlotSprite { get; private set; }
    public Sprite PlacementSlotSprite { get; private set; }
    public Sprite TagSlotSprite { get; private set; }

    public bool PawnActive { get { return _characterPawn.activeSelf; } }
    public Vector3 PawnPosition { get { return _characterPawn.transform.position; } }

    public CharacterData(string name, GameObject pawn)
    {
        Name = name;

        _characterPawn = pawn;

        _characterStatus = pawn.GetComponent<CharacterStatus>();

        InventorySlotSprite = Resources.Load<Sprite>($"Images/Inventory Slot/{Name}");
        PlacementSlotSprite = Resources.Load<Sprite>($"Images/Placement Slot/{Name}");
        TagSlotSprite = Resources.Load<Sprite>($"Images/Tag Slot/{Name}");
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
