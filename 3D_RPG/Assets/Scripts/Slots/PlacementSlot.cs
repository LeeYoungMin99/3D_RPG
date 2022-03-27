using System;
using UnityEngine;

public class PlacementSlot : CharacterSlot
{
    private CharacterData _selectedCharacterData;

    public event EventHandler<OnSlotClickEventArgs> PlacementSlotClickEvent;

    private void OnDisable()
    {
        _selectedCharacterData = null;

        _slotButton.interactable = false;
    }

    private void ChangeCharacterData(CharacterData characterData)
    {
        _characterData = characterData;

        _image.sprite = characterData.PlacementSlotSprite;
    }

    private void SetSelectedCharacterData(object sender, OnSlotClickEventArgs args)
    {
        _selectedCharacterData = args.CharacterData;

        _eventArgs = args;

        _slotButton.interactable = true;
    }

    private void CheckTheSameCharacterData(object sender, EventArgs args)
    {
        if (_selectedCharacterData != _characterData) return;

        _characterData = null;

        _image.sprite = null;

        PlacementSlotClickEvent?.Invoke(sender, null);
    }

    private void DisableClick(object sender, EventArgs args)
    {
        _slotButton.interactable = false;
    }

    protected override void Awake()
    {
        base.Awake();

        _characterInventorySlotManager.InventorySlotClickEvent -= SetSelectedCharacterData;
        _characterInventorySlotManager.InventorySlotClickEvent += SetSelectedCharacterData;

        _characterInventorySlotManager.PlacementSlotClickEvent -= DisableClick;
        _characterInventorySlotManager.PlacementSlotClickEvent += DisableClick;

        _characterInventorySlotManager.PlacementSlotClickEvent -= CheckTheSameCharacterData;
        _characterInventorySlotManager.PlacementSlotClickEvent += CheckTheSameCharacterData;
    }

    protected override void OnClick()
    {
        PlacementSlotClickEvent?.Invoke(this, _eventArgs);

        ChangeCharacterData(_selectedCharacterData);
    }
}
