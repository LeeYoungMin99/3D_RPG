using UnityEngine;

public class PlacementSlot : CharacterInventorySlot
{
    private void Awake()
    {
        _slotButton.onClick.RemoveListener(OnClick);
        _slotButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (true == _characterInventorySlotManager.CurSelectCharacter.bIsDeployment)
        {
            _characterInventorySlotManager.WithdrawCharacter(_characterInventorySlotManager.CurSelectCharacter);
        }

        _character = _characterInventorySlotManager.CurSelectCharacter;
        _characterInventorySlotManager.CurSelectCharacter = null;

        _character.bIsDeployment = true;

        RefreshSlotData();

        _characterInventorySlotManager.SetInteractablePlacementSlots(false);
    }

    private void OnEnable()
    {
        RefreshSlotData();
    }

    public bool CheckCharacterInfo(Character data)
    {
        if (data == _character)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void WithdrawCharacter()
    {
        _character.bIsDeployment = false;
        _character = null;

        RefreshSlotData();
    }

    private void RefreshSlotData()
    {
        if (null != _character)
        {
            _image.sprite = Resources.Load<Sprite>($"Images/Deployment Slot/{_character.Name}");
            _text.text = $"Level : {_character.Level}\nName : {_character.Name}\nHP : {_character.MaxHP}\nATK : {_character.ATK}\nDEF : {_character.DEF}";
        }
        else
        {
            _image.sprite = null;
            _text.text = null;
        }
    }
}
