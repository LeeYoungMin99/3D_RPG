using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : CharacterSlot
{
    private List<CharacterData> _characterDatas = new List<CharacterData>();
    private int _characterDatasCount = 0;

    public event EventHandler<OnSlotClickEventArgs> InventorySlotClickEvent;

    private void OnEnable()
    {
        if (0 == _characterDatasCount)
        {
            _image.sprite = null;

            _slotButton.interactable = false;
        }
        else
        {
            SetCharacterData(_characterDatas[0]);

            if (true == _characterData.CheckDeath())
            {
                _slotButton.interactable = false;
            }
            else
            {
                _slotButton.interactable = true;
            }
        }
    }

    private void SetCharacterData(CharacterData characterData)
    {
        _characterData = characterData;

        _image.sprite = _characterData.InventorySlotSprite;
    }

    protected override void Awake()
    {
        base.Awake();

        _eventArgs = new OnSlotClickEventArgs();
    }

    protected override void OnClick()
    {
        _eventArgs.CharacterData = _characterData;

        InventorySlotClickEvent?.Invoke(this, _eventArgs);
    }

    public void AddCharacterData(CharacterData characterData)
    {
        _characterDatas.Add(characterData);

        ++_characterDatasCount;
    }
}
