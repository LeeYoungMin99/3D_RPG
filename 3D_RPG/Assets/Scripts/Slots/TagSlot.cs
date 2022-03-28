using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagSlot : CharacterSlot
{
    [Header("Character Status")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _experienceBar;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _nameText;

    private SkillEventArgs _skillEventArgs = new SkillEventArgs();

    public event EventHandler<OnSlotClickEventArgs> TagSlotClickEvent;
    public event EventHandler<SkillEventArgs> CharacterDataChangeEvent;

    private void OnEnable()
    {
        if (true == CheckCharacterDataIsNull()) return;

        if (0 != transform.GetSiblingIndex())
        {
            _slotButton.interactable = true;
        }
        else
        {
            _slotButton.interactable = false;

            _characterData.SetCharacterPawnActive(true);
        }
    }

    private void SetAsFirstSibling(object sender, EventArgs args)
    {
        transform.SetAsFirstSibling();
    }

    private void DisableCharacterPawn(object sender, EventArgs args)
    {
        if (null == _characterData) return;

        _characterData.SetCharacterPawnActive(false);
    }

    private void EnableClick(object sender, EventArgs args)
    {
        if (true == CheckCharacterDataIsNull()) return;

        _slotButton.interactable = true;
    }

    private void ReplacementCharacter(object sender, OnSlotClickEventArgs args)
    {
        if (true == CheckCharacterDataIsNull()) return;

        if (false == _characterData.PawnActive) return;

        args.CharacterData.SetPawnPosition(_characterData.PawnPosition);
    }

    private void SetUI(object sender, DataChangeEventArgs args)
    {
        _healthBar.value = args.NormalizedCurHP;
        _experienceBar.value = args.CurExperience;
        _levelText.text = $"Lv.{args.Level}";
        _nameText.text = $"{args.Name}";
    }

    protected override void Awake()
    {
        base.Awake();

        _characterInventorySlotManager.PlacementSlotClickEvent -= SetAsFirstSibling;
        _characterInventorySlotManager.PlacementSlotClickEvent += SetAsFirstSibling;

        _characterInventorySlotManager.PlacementSlotClickEvent -= DisableCharacterPawn;
        _characterInventorySlotManager.PlacementSlotClickEvent += DisableCharacterPawn;

        _characterInventorySlotManager.TagSlotClickEvent -= ReplacementCharacter;
        _characterInventorySlotManager.TagSlotClickEvent += ReplacementCharacter;

        _characterInventorySlotManager.TagSlotClickEvent -= DisableCharacterPawn;
        _characterInventorySlotManager.TagSlotClickEvent += DisableCharacterPawn;

        _characterInventorySlotManager.TagSlotClickEvent -= EnableClick;
        _characterInventorySlotManager.TagSlotClickEvent += EnableClick;
    }

    protected override void OnClick()
    {
        TagSlotClickEvent?.Invoke(this, _eventArgs);

        transform.SetAsFirstSibling();

        _slotButton.interactable = false;

        _characterData.SetCharacterPawnActive(true);
    }

    public bool CheckCharacterDataIsNull()
    {
        if (null == _characterData) return true;

        return false;
    }

    public void SetCharacterData(object sender, OnSlotClickEventArgs args)
    {
        if (null != _characterData)
        {
            _characterData.CharacterStatus.OnChangeDataEvent -= SetUI;
        }

        if (null == args)
        {
            _characterData = null;

            _image.sprite = null;

            _eventArgs = null;

            _slotButton.interactable = false;

            _healthBar.gameObject.SetActive(false);
            _experienceBar.gameObject.SetActive(false);
        }
        else
        {
            _characterData = args.CharacterData;

            _image.sprite = _characterData.TagSlotSprite;

            _eventArgs = args;

            _slotButton.interactable = true;

            _characterData.CharacterStatus.OnChangeDataEvent -= SetUI;
            _characterData.CharacterStatus.OnChangeDataEvent += SetUI;

            _healthBar.gameObject.SetActive(true);
            _experienceBar.gameObject.SetActive(true);

            _skillEventArgs.CharacterData = _characterData;
            CharacterDataChangeEvent?.Invoke(this, _skillEventArgs);

            _characterData.CharacterStatus.CallChangeDataEvent();
        }
    }
}
