using System;
using UnityEngine;

public class CharacterData
{
    private GameObject _characterPawn;
    private CharacterStatus _characterStatus;
    private OnChangeDeploy _onChangeDeploy;

    public delegate void OnChangeDeploy();

    public string Name { get; private set; }
    public Sprite InventorySlotSprite { get; private set; }
    public Sprite PlacementSlotSprite { get; private set; }
    public Sprite TagSlotSprite { get; private set; }
    public OnChangeDeploy AddDelegateOnChangeDeploy
    {
        get
        {
            return _onChangeDeploy;
        }
        set
        {
            _onChangeDeploy?.Invoke();
            _onChangeDeploy -= value;
            _onChangeDeploy += value;
        }
    }

    public OnChangeDeploy SubtractDelegateOnChangeDeploy
    {
        get
        {
            return _onChangeDeploy;
        }
        set
        {
            _onChangeDeploy -= value;
        }
    }
    public CharacterData(string name)
    {
        Name = name;

        InventorySlotSprite = Resources.Load<Sprite>($"Images/Inventory Slot/{Name}");
        PlacementSlotSprite = Resources.Load<Sprite>($"Images/Placement Slot/{Name}");
        TagSlotSprite = Resources.Load<Sprite>($"Images/Tag Slot/{Name}");
    }

    public void EnableCharacter()
    {
        _characterPawn.SetActive(true);
    }

    public void DisableCharacter()
    {
        _characterPawn.SetActive(false);
    }

    public void SetCharacterPawn(GameObject pawn)
    {
        _characterPawn = pawn;
        _characterStatus = pawn.GetComponent<CharacterStatus>();
    }
}
