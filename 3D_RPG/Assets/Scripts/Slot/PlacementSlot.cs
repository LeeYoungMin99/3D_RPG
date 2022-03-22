using UnityEngine;

public class PlacementSlot : CharacterSlot
{
    protected override void OnClick()
    {
        _characterInventorySlotManager.SetInteractablePlacementSlots(false);

        if (_character == _characterInventorySlotManager.CurSelectCharacter)
        {
            return;
        }

        _characterInventorySlotManager.OnClickPlacementSlot(_index);
    }

    public void WithdrawCharacter()
    {
        _character.SubtractDelegateOnChangeDeploy = WithdrawCharacter;

        _characterInventorySlotManager.WithdrawCharacter(_index);

        _character = null;

        _image.sprite = null;
    }

    public override void ChangeCharacter(CharacterData character)
    {
        if (null != _character)
        {
            WithdrawCharacter();
        }

        _character = character;

        _image.sprite = _character.PlacementSlotSprite;

        _character.AddDelegateOnChangeDeploy = WithdrawCharacter;
    }
}
