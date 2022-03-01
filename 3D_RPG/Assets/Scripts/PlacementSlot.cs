using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementSlot : CharacterInventorySlot
{
    [SerializeField] private Text _text;

    protected override void Awake()
    {
        base.Awake();

        _path = $"Images/Deployment Slot/{CharacterInfo.Name}";

        SlotButton.onClick.RemoveListener(OnClick);
        SlotButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (true == _characterInventory._curSelectCharacterData.IsDeployment)
        {
            _characterInventory.WithdrawCharacter(_characterInventory._curSelectCharacterData);
        }

        CharacterInfo = _characterInventory._curSelectCharacterData;
        _characterInventory._curSelectCharacterData = null;

        CharacterInfo.IsDeployment = true;

        SetSlotData();

        _characterInventory.SetInteractableDeploymentSlots(false);
    }

    private void OnEnable()
    {
        SetSlotData();
    }

    public bool CheckCharacterInfo(CharacterData data)
    {
        if (data == CharacterInfo)
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
        CharacterInfo.IsDeployment = false;
        CharacterInfo = null;

        SetSlotData();
    }

    private void SetSlotData()
    {
        if (null != CharacterInfo)
        {
            _image.sprite = Resources.Load<Sprite>(_path);
            _text.text = $"Level : {CharacterInfo.Level}\nName : {CharacterInfo.Name}\nHP : {CharacterInfo.MaxHP}\nATK : {CharacterInfo.ATK}\nDEF : {CharacterInfo.DEF}";
        }
        else
        {
            _image.sprite = null;
            _text.text = "";
        }
    }
}
