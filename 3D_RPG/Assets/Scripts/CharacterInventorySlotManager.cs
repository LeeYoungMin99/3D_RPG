using System.Collections.Generic;
using UnityEngine;

public class CharacterInventorySlotManager : MonoBehaviour
{
    [SerializeField] private const int MAX_SHOW_INVENTORY_SLOT = 10;
    [SerializeField] private const int MAX_PLACEMENT_SLOT_COUNT = 3;

    [SerializeField] private PlacementSlot[] _placementSlot = new PlacementSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private InventorySlot[] _inventorySlot = new InventorySlot[MAX_SHOW_INVENTORY_SLOT];
    [SerializeField] private CharacterTagSlot[] _characterTagSlot = new CharacterTagSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private GameObject _player;
    [SerializeField] private int _maxInventorySize;

    private int _curCharacterCount = 0;
    private int _curInventoryPage = 1;
    private bool _bIsClickPlacementSlot = false;

    private List<Character> _characterList = new List<Character>();

    public Character CurSelectCharacter;

    private void Awake()
    {
        SortHierachyTagSlots();

        ObtainCharacter("Sword Man");
        ObtainCharacter("Archer");
        ObtainCharacter("Gunner");
        ObtainCharacter("Magician");
    }

    private void OnEnable()
    {
        RefreshInventory();

        _bIsClickPlacementSlot = false;
    }

    private void OnDisable()
    {
        if (true == _bIsClickPlacementSlot)
        {
            SortHierachyTagSlots();
        }
    }

    private void RefreshInventory()
    {
        int dataIndex = (_curInventoryPage - 1) * 10;
        int maxIndex = _curInventoryPage * 10;

        for (int slotIndex = 0; maxIndex > dataIndex; ++dataIndex, ++slotIndex)
        {
            if (dataIndex < _curCharacterCount)
            {
                _inventorySlot[slotIndex].ChangeCharacter(_characterList[dataIndex]);
            }
            else
            {
                _inventorySlot[slotIndex].ChangeCharacter(null);
            }
        }
    }

    private void SortHierachyTagSlots()
    {
        for (int i = MAX_PLACEMENT_SLOT_COUNT; i > 0; --i)
        {
            _characterTagSlot[i - 1].SortHierarchy();
        }
    }

    public bool ObtainCharacter(string name)
    {
        if (_curCharacterCount >= _maxInventorySize)
        {
            return false;
        }

        Character character = new Character(name);
        GameObject pawn = Instantiate(Resources.Load<GameObject>($"Characters/{name}"), _player.transform.position, _player.transform.rotation);

        pawn.transform.SetParent(_player.transform);

        _characterList.Add(character);
        ++_curCharacterCount;

        character.SetCharacterPawn(pawn);
        character.DisableCharacter();

        return true;
    }

    public void SetInteractablePlacementSlots(bool b)
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _placementSlot[i].SetInteractabletSlotButton(b);
        }
    }

    public void OnClickPlacementSlot(int index)
    {
        _bIsClickPlacementSlot = true;

        _placementSlot[index].ChangeCharacter(CurSelectCharacter);
        _characterTagSlot[index].ChangeCharacter(CurSelectCharacter);
    }

    public void OnClickTagSlot()
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _characterTagSlot[i].OnChangeIndex();
        }
    }

    public void WithdrawCharacter(int index)
    {
        _characterTagSlot[index].WithdrawCharacter();
    }
}
