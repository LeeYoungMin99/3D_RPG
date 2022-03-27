using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventorySlotManager : MonoBehaviour
{
    [SerializeField] private PlacementSlot[] _placementSlot = new PlacementSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private InventorySlot[] _inventorySlot = new InventorySlot[MAX_SHOW_INVENTORY_SLOT];
    [SerializeField] private TagSlot[] _tagSlot = new TagSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private GameObject _player;
    [SerializeField] private int _maxInventorySize;

    private int _curCharacterCount = 0;

    public event EventHandler<OnSlotClickEventArgs> InventorySlotClickEvent;
    public event EventHandler<OnSlotClickEventArgs> TagSlotClickEvent;
    public event EventHandler<EventArgs> PlacementSlotClickEvent;

    private const int MAX_SHOW_INVENTORY_SLOT = 10;
    private const int MAX_PLACEMENT_SLOT_COUNT = 3;

    private void Awake()
    {
        ObtainCharacter("Sword Man");
        ObtainCharacter("Archer");
        ObtainCharacter("Magician");

        for (int i = 0; i < MAX_SHOW_INVENTORY_SLOT; ++i)
        {
            _inventorySlot[i].gameObject.SetActive(true);

            _inventorySlot[i].InventorySlotClickEvent -= OnClickInventorySlot;
            _inventorySlot[i].InventorySlotClickEvent += OnClickInventorySlot;
        }

        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _placementSlot[i].gameObject.SetActive(true);

            _placementSlot[i].PlacementSlotClickEvent -= OnClickPlacementSlot;
            _placementSlot[i].PlacementSlotClickEvent += OnClickPlacementSlot;

            _placementSlot[i].PlacementSlotClickEvent -= _tagSlot[i].SetCharacterData;
            _placementSlot[i].PlacementSlotClickEvent += _tagSlot[i].SetCharacterData;

            _tagSlot[i].TagSlotClickEvent -= OnClickTagSlot;
            _tagSlot[i].TagSlotClickEvent += OnClickTagSlot;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            if (true == _tagSlot[i].CheckCharacterDataIsNull())
            {
                _tagSlot[i].transform.SetAsLastSibling();
            }
        }
    }

    private void OnClickInventorySlot(object sender, OnSlotClickEventArgs args)
    {
        InventorySlotClickEvent?.Invoke(sender, args);
    }

    private void OnClickPlacementSlot(object sender, EventArgs args)
    {
        PlacementSlotClickEvent?.Invoke(sender, EventArgs.Empty);
    }

    private void OnClickTagSlot(object sender, OnSlotClickEventArgs args)
    {
        TagSlotClickEvent?.Invoke(sender, args);
    }

    public bool ObtainCharacter(string name)
    {
        if (_curCharacterCount >= _maxInventorySize) return false;


        GameObject pawn = Instantiate(Resources.Load<GameObject>($"Character/{name}"), _player.transform.position, _player.transform.rotation);

        pawn.transform.SetParent(_player.transform);

        CharacterData character = new CharacterData(name, pawn);

        _inventorySlot[_curCharacterCount].AddCharacterData(character);

        ++_curCharacterCount;

        return true;
    }
}
