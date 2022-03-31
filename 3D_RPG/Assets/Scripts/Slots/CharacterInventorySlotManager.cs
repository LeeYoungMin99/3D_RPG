using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventorySlotManager : MonoBehaviour
{
    [SerializeField] private PlacementSlot[] _placementSlot = new PlacementSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private InventorySlot[] _inventorySlot = new InventorySlot[MAX_SHOW_INVENTORY_SLOT];
    [SerializeField] private TagSlot[] _tagSlot = new TagSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _cinemachineCameraRoot;
    [SerializeField] private int _maxInventorySize;

    private int _curCharacterCount = 0;

    public event EventHandler<SlotClickEventArgs> InventorySlotClickEvent;
    public event EventHandler<SlotClickEventArgs> TagSlotClickEvent;
    public event EventHandler<EventArgs> PlacementSlotClickEvent;

    private const int MAX_SHOW_INVENTORY_SLOT = 10;
    private const int MAX_PLACEMENT_SLOT_COUNT = 3;

    private void Awake()
    {
        for (int i = 0; i < MAX_SHOW_INVENTORY_SLOT; ++i)
        {
            _inventorySlot[i].gameObject.SetActive(true);

            _inventorySlot[i].InventorySlotClickEvent -= OnClickInventorySlot;
            _inventorySlot[i].InventorySlotClickEvent += OnClickInventorySlot;
        }

        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _placementSlot[i].gameObject.SetActive(true);
            _tagSlot[i].gameObject.SetActive(true);

            _placementSlot[i].PlacementSlotClickEvent -= OnClickPlacementSlot;
            _placementSlot[i].PlacementSlotClickEvent += OnClickPlacementSlot;

            _placementSlot[i].PlacementSlotClickEvent -= _tagSlot[i].SetCharacterData;
            _placementSlot[i].PlacementSlotClickEvent += _tagSlot[i].SetCharacterData;

            _tagSlot[i].TagSlotClickEvent -= OnClickTagSlot;
            _tagSlot[i].TagSlotClickEvent += OnClickTagSlot;
        }

        QuestManager.Instance.QuestCompleteEvent -= ObtainCharacterReward;
        QuestManager.Instance.QuestCompleteEvent += ObtainCharacterReward;
    }

    private void Start()
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            for (int j = 0; j < MAX_PLACEMENT_SLOT_COUNT; ++j)
            {
                _placementSlot[i].PlacementSlotClickEvent -= _tagSlot[j].CheckCharacterDataAndSetAsLastSibling;
                _placementSlot[i].PlacementSlotClickEvent += _tagSlot[j].CheckCharacterDataAndSetAsLastSibling;
            }

            for (int j = 0; j < MAX_PLACEMENT_SLOT_COUNT; ++j)
            {
                _placementSlot[i].PlacementSlotClickEvent -= _tagSlot[j].CheckIndex;
                _placementSlot[i].PlacementSlotClickEvent += _tagSlot[j].CheckIndex;
            }
        }
    }

    private void OnClickInventorySlot(object sender, SlotClickEventArgs args)
    {
        InventorySlotClickEvent?.Invoke(sender, args);
    }

    private void OnClickPlacementSlot(object sender, EventArgs args)
    {
        PlacementSlotClickEvent?.Invoke(sender, EventArgs.Empty);
    }

    private void OnClickTagSlot(object sender, SlotClickEventArgs args)
    {
        TagSlotClickEvent?.Invoke(sender, args);
    }

    public void ObtainCharacterReward(object sender, QuestCompleteEventArgs args)
    {
        if (null == args.CharacterReward) return;

        foreach (string characterReward in args.CharacterReward)
        {
            ObtainCharacter(characterReward);
        }
    }

    public bool ObtainCharacter(string name)
    {
        if (_curCharacterCount >= _maxInventorySize) return false;

        GameObject pawn = Instantiate(Resources.Load<GameObject>($"Character/{name}"), _player.transform.position, _player.transform.rotation, _player.transform);
        pawn.name = name;
        pawn.layer = 6;
        pawn.tag = "Player";

        pawn.AddComponent<PlayerTargetManager>();
        pawn.AddComponent<PlayerDeath>();

        pawn.SetActive(true);
        pawn.SetActive(false);

        Instantiate(_cinemachineCameraRoot, pawn.transform);

        CharacterData character = new CharacterData(name, pawn);

        QuestManager.Instance.QuestCompleteEvent -= character.CharacterStatus.ObtainExperienceReward;
        QuestManager.Instance.QuestCompleteEvent += character.CharacterStatus.ObtainExperienceReward;

        _inventorySlot[_curCharacterCount].AddCharacterData(character);

        ++_curCharacterCount;

        return true;
    }
}
