using System.Collections.Generic;
using UnityEngine;

public class CharacterTagSlot : CharacterSlot
{
    private void OnEnable()
    {
        if(null != _character)
        {
            OnChangeIndex();
        }
    }

    protected override void OnClick()
    {
        transform.SetAsFirstSibling();

        _characterInventorySlotManager.OnClickTagSlot();
    }

    public override void ChangeCharacter(Character character)
    {
        _character = character;

        _image.sprite = _character.TagSlotSprite;
    }

    public void WithdrawCharacter()
    {
        _character.DisableCharacter();

        _character = null;

        _image.sprite = null;
    }

    public void SortHierarchy()
    {
        if(null != _character)
        {
            transform.SetAsFirstSibling();
        }
        else
        {
            transform.SetAsLastSibling();
        }
    }

    public void OnChangeIndex()
    {
        if (0 == transform.GetSiblingIndex())
        {
            _character.EnableCharacter();
            SetInteractabletSlotButton(false);
        }
        else
        {
            _character.DisableCharacter();
            SetInteractabletSlotButton(true);
        }
    }
}
