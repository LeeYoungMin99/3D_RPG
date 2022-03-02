using UnityEngine;

public class CharacterSlot : CharacterInventorySlot
{
    private void Awake()
    {
        _slotButton.onClick.RemoveListener(OnClick);
        _slotButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _characterInventorySlotManager.CurSelectCharacter = _character;

        _characterInventorySlotManager.SetInteractablePlacementSlots(true);
    }

    private void OnEnable()
    {
        if (null == _character)
        {
            SetInteractabletSlotButton(false);
            _image.sprite = null;
            _text.text = null;
        }
        else
        {
            SetInteractabletSlotButton(true);
            _image.sprite = Resources.Load<Sprite>($"Images/Character Slot/{_character.Name}");
            _text.text = $"Level : {_character.Level}";
        }
    }
}
