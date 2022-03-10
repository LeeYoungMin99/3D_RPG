using System;
using UnityEngine;

public class Character
{
    public Character(string name)
    {
        Name = name;

        InventorySlotSprite = Resources.Load<Sprite>($"Images/Inventory Slot/{Name}");
        PlacementSlotSprite = Resources.Load<Sprite>($"Images/Placement Slot/{Name}");
        TagSlotSprite = Resources.Load<Sprite>($"Images/Tag Slot/{Name}");
    }
    public delegate void OnChangeDeploy();

    private GameObject _characterPwan;
    private OnChangeDeploy _onChangeDeploy;

    public string Name { get; private set; }

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

    public Sprite InventorySlotSprite { get; private set; }
    public Sprite PlacementSlotSprite { get; private set; }
    public Sprite TagSlotSprite { get; private set; }

    public void EnableCharacter()
    {
        _characterPwan.SetActive(true);
    }

    public void DisableCharacter()
    {
        _characterPwan.SetActive(false);
    }

    public void SetCharacterPawn(GameObject prefab)
    {
        _characterPwan = prefab;
    }
}
