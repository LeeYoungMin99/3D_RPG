using UnityEngine;

public class PlacementSlot : CharacterSlot
{
    protected override void OnClick()
    {
        if (true == _characterInventorySlotManager.CurSelectCharacter.bIsDeployment)
        {
            _characterInventorySlotManager.WithdrawCharacter(_characterInventorySlotManager.CurSelectCharacter);
        }

        Character = _characterInventorySlotManager.CurSelectCharacter;
        _characterInventorySlotManager.CurSelectCharacter = null;

        Character.bIsDeployment = true;

        RefreshSlotData();

        _characterInventorySlotManager.OnClickPlacementSlot(transform.GetSiblingIndex());

        _characterInventorySlotManager.SetInteractablePlacementSlots(false);
    }

    private void OnEnable()
    {
        RefreshSlotData();
    }

    public bool CheckCharacterInfo(Character data)
    {
        if (data == Character)
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
        Character.bIsDeployment = false;
        Character = null;

        RefreshSlotData();
    }

    private void RefreshSlotData()
    {
        if (null != Character)
        {
            _image.sprite = Resources.Load<Sprite>($"Images/Placement Slot/{Character.Name}");
            _text.text = $"Level : {Character.Level}\nName : {Character.Name}\nHP : {Character.MaxHP}\nATK : {Character.ATK}\nDEF : {Character.DEF}";
        }
        else
        {
            _image.sprite = null;
            _text.text = null;
        }
    }
}
