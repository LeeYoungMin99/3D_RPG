using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : CharacterSlot
{
    [SerializeField] private Text _levelText;

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

    private void SetLevelText(object sender, DataChangeEventArgs args)
    {
        _levelText.text = $"Lv.{args.Level}";
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

        characterData.CharacterStatus.OnChangeDataEvent -= SetLevelText;
        characterData.CharacterStatus.OnChangeDataEvent += SetLevelText;

        ++_characterDatasCount;
    }
}
