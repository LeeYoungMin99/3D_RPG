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

        SlotButton.onClick.RemoveListener(OnClick);
        SlotButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (true == _characterInventory._curSelectCharacterData.IsDeployment)
        {
            _characterInventory.DisplacementCharacter(_characterInventory._curSelectCharacterData);
        }

        CharacterInfo = _characterInventory._curSelectCharacterData;
        _characterInventory._curSelectCharacterData = null;

        CharacterInfo.IsDeployment = true;

        SetSlotDatas();

        _characterInventory.SetInteractableDeploymentSlots(false);
    }

    private void OnEnable()
    {
        SetSlotDatas();
    }

    public void DisplacementCharacter(CharacterData data)
    {
        if (data == CharacterInfo)
        {
            data.IsDeployment = false;
            CharacterInfo = null;

            SetSlotDatas();
        }
    }

    private void SetSlotDatas()
    {
        if (null != CharacterInfo)
        {
            _image.sprite = Resources.Load<Sprite>($"Images/Deployment Slot/{CharacterInfo.Name}");
            _text.text = $"Level : {CharacterInfo.Level}\nName : {CharacterInfo.Name}\nHP : {CharacterInfo.MaxHP}\nATK : {CharacterInfo.ATK}\nDEF : {CharacterInfo.DEF}";
        }
        else
        {
            _image.sprite = null;
            _text.text = "";
        }
    }
}
