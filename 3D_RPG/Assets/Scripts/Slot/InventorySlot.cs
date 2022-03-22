using UnityEngine;

public class InventorySlot : CharacterSlot
{
    protected override void OnClick()
    {
        _characterInventorySlotManager.CurSelectCharacter = _character;

        _characterInventorySlotManager.SetInteractablePlacementSlots(true);
    }

    public override void ChangeCharacter(CharacterData character)
    {
        _character = character;

        if (null != character)
        {
            _image.sprite = _character.InventorySlotSprite;

            SetInteractabletSlotButton(true);
        }
        else
        {
            _image.sprite = null;

            SetInteractabletSlotButton(false);
        }
    }
}
