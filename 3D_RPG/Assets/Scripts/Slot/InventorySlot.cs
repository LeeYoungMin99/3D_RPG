using UnityEngine;

public class InventorySlot : CharacterSlot
{
    protected override void OnClick()
    {
        _characterInventorySlotManager.CurSelectCharacter = Character;

        _characterInventorySlotManager.SetInteractablePlacementSlots(true);
    }

    private void OnEnable()
    {
        if (null == Character)
        {
            SetInteractabletSlotButton(false);
            _image.sprite = null;
            _text.text = null;
        }
        else
        {
            SetInteractabletSlotButton(true);
            _image.sprite = Resources.Load<Sprite>($"Images/Character Slot/{Character.Name}");
            _text.text = $"Level : {Character.Level}";
        }
    }
}
