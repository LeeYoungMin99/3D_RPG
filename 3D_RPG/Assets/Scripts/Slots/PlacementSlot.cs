using System;
using UnityEngine;
using UnityEngine.UI;

public class PlacementSlot : CharacterSlot
{
    [SerializeField] Text _statusText;

    private CharacterData _selectedCharacterData;

    public event EventHandler<OnSlotClickEventArgs> PlacementSlotClickEvent;

    private void OnDisable()
    {
        _selectedCharacterData = null;

        _slotButton.interactable = false;
    }

    private void ChangeCharacterData(CharacterData characterData)
    {
        if(null != _characterData)
        {
            _characterData.CharacterStatus.OnChangeDataEvent -= SetStatusText;
        }

        _characterData = characterData;

        _image.sprite = characterData.PlacementSlotSprite;

        _characterData.CharacterStatus.OnChangeDataEvent -= SetStatusText;
        _characterData.CharacterStatus.OnChangeDataEvent += SetStatusText;

        _characterData.CharacterStatus.CallChangeDataEvent();
    }

    private void SetSelectedCharacterData(object sender, OnSlotClickEventArgs args)
    {
        _selectedCharacterData = args.CharacterData;

        _eventArgs = args;

        _slotButton.interactable = true;
    }

    private void CheckTheSameCharacterData(object sender, EventArgs args)
    {
        if (_selectedCharacterData != _characterData) return;

        _characterData = null;

        _image.sprite = null;

        PlacementSlotClickEvent?.Invoke(sender, null);
    }

    private void DisableClick(object sender, EventArgs args)
    {
        _slotButton.interactable = false;
    }

    private void SetStatusText(object sender, DataChangeEventArgs args)
    {
        _statusText.text = $" Lv : {args.Level}\n ATK : {args.ATK}\n HP\n {args.MaxHP} / {args.CurHP}";
    }

    protected override void Awake()
    {
        base.Awake();

        _characterInventorySlotManager.InventorySlotClickEvent -= SetSelectedCharacterData;
        _characterInventorySlotManager.InventorySlotClickEvent += SetSelectedCharacterData;

        _characterInventorySlotManager.PlacementSlotClickEvent -= DisableClick;
        _characterInventorySlotManager.PlacementSlotClickEvent += DisableClick;

        _characterInventorySlotManager.PlacementSlotClickEvent -= CheckTheSameCharacterData;
        _characterInventorySlotManager.PlacementSlotClickEvent += CheckTheSameCharacterData;
    }

    protected override void OnClick()
    {
        PlacementSlotClickEvent?.Invoke(this, _eventArgs);

        ChangeCharacterData(_selectedCharacterData);
    }
}
