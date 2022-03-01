using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : CharacterInventorySlot
{
    protected override void Awake()
    {
        base.Awake();

        _path = $"Images/Character Slot/{CharacterInfo.Name}";

        SlotButton.onClick.RemoveListener(OnClick);
        SlotButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _characterInventory._curSelectCharacterData = CharacterInfo;

        _characterInventory.SetInteractableDeploymentSlots(true);
    }

    private void OnEnable()
    {
        if (null == CharacterInfo)
        {
            SlotButton.interactable = false;
            _image.sprite = null;
        }
        else
        {
            SlotButton.interactable = true;
            _image.sprite = Resources.Load<Sprite>(_path);
        }
    }
}
