using System;
using System.Collections.Generic;
using UnityEngine;

public class TagSlot : CharacterSlot
{
    public event EventHandler<OnSlotClickEventArgs> TagSlotClickEvent;

    private void OnEnable()
    {
        if (true == CheckCharacterDataIsNull()) return;

        if (0 != transform.GetSiblingIndex())
        {
            _slotButton.interactable = true;
        }
        else
        {
            _slotButton.interactable = false;

            _characterData.SetCharacterPawnActive(true);
        }
    }

    private void SetAsLastSibling(object sender, EventArgs args)
    {
        transform.SetAsLastSibling();
    }

    private void DisableCharacterPawn(object sender, EventArgs args)
    {
        if (null == _characterData) return;

        _characterData.SetCharacterPawnActive(false);
    }

    private void EnableClick(object sender, EventArgs args)
    {
        if (true == CheckCharacterDataIsNull()) return;

        _slotButton.interactable = true;
    }

    private void ReplacementCharacter(object sender, OnSlotClickEventArgs args)
    {
        if (true == CheckCharacterDataIsNull()) return;

        if (false == _characterData.PawnActive) return;

        args.CharacterData.SetPawnPosition(_characterData.PawnPosition);
    }

    protected override void Awake()
    {
        base.Awake();

        _characterInventorySlotManager.PlacementSlotClickEvent -= SetAsLastSibling;
        _characterInventorySlotManager.PlacementSlotClickEvent += SetAsLastSibling;

        _characterInventorySlotManager.PlacementSlotClickEvent -= DisableCharacterPawn;
        _characterInventorySlotManager.PlacementSlotClickEvent += DisableCharacterPawn;

        _characterInventorySlotManager.TagSlotClickEvent -= ReplacementCharacter;
        _characterInventorySlotManager.TagSlotClickEvent += ReplacementCharacter;

        _characterInventorySlotManager.TagSlotClickEvent -= DisableCharacterPawn;
        _characterInventorySlotManager.TagSlotClickEvent += DisableCharacterPawn;

        _characterInventorySlotManager.TagSlotClickEvent -= EnableClick;
        _characterInventorySlotManager.TagSlotClickEvent += EnableClick;
    }

    protected override void OnClick()
    {
        TagSlotClickEvent?.Invoke(this, _eventArgs);

        transform.SetAsFirstSibling();

        _slotButton.interactable = false;

        _characterData.SetCharacterPawnActive(true);
    }

    public bool CheckCharacterDataIsNull()
    {
        if (null == _characterData) return true;

        return false;
    }

    public void SetCharacterData(object sender, OnSlotClickEventArgs args)
    {
        if (null == args)
        {
            _characterData = null;

            _image.sprite = null;

            _eventArgs = null;

            _slotButton.interactable = false;
        }
        else
        {
            _characterData = args.CharacterData;

            _image.sprite = _characterData.TagSlotSprite;

            _eventArgs = args;

            _slotButton.interactable = true;
        }
    }
}
