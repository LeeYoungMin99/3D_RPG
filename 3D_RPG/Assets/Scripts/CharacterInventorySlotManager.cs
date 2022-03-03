using System.Collections.Generic;
using UnityEngine;

public class CharacterInventorySlotManager : MonoBehaviour
{
    [SerializeField] private const int MAX_SHOW_INVENTORY_SLOT = 10;
    [SerializeField] private const int MAX_PLACEMENT_SLOT_COUNT = 3;

    [SerializeField] private PlacementSlot[] _placementSlot = new PlacementSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private InventorySlot[] _inventorySlot = new InventorySlot[MAX_SHOW_INVENTORY_SLOT];
    [SerializeField] private CharacterTagSlot[] _characterTagSlot = new CharacterTagSlot[MAX_PLACEMENT_SLOT_COUNT];
    [SerializeField] private GameObject _tagSlotParent;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _maxInventorySize = 20;

    private int _curCharacterCount = 0;
    private int _curInventoryPage = 1;

    private List<Character> _characterList = new List<Character>();

    public Character CurSelectCharacter;
    [SerializeField] GameObject _player;

    private void Awake()
    {
        _curInventoryPage = 1;

        ObtainCharacter(new Character("Sword Man", 1, 1, 1));
        ObtainCharacter(new Character("Archer", 1, 1, 1));
        ObtainCharacter(new Character("Gunner", 1, 1, 1));
        ObtainCharacter(new Character("Magician", 1, 1, 1));
    }

    private void OnEnable()
    {
        RefreshInventory();

        _canvas.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _canvas.gameObject.SetActive(false);
    }

    private void RefreshInventory()
    {
        int dataIndex = (_curInventoryPage - 1) * 10;
        int maxIndex = _curInventoryPage * 10;

        _curCharacterCount = _characterList.Count;

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
    public void ObtainCharacter(Character data)
    {
        _characterList.Add(data);

        data.CharacterPwan = Instantiate(Resources.Load<GameObject>($"Characters/{data.Name}"), _player.transform.position, _player.transform.rotation);
        data.CharacterPwan.SetActive(false);
        data.CharacterPwan.transform.SetParent(_player.transform);
    }

    public void WithdrawCharacter(Character data)
    {
        for (int i = 0; i < 3; ++i)
        {
            if (true == _placementSlot[i].CheckCharacterInfo(data))
            {
                _placementSlot[i].WithdrawCharacter();

                break;
            }
        }
    }

    public void SetInteractablePlacementSlots(bool b)
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _placementSlot[i].SetInteractabletSlotButton(b);
        }
    }

    public void SetInteractableTagSlots(bool b)
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _characterTagSlot[i].SetInteractabletSlotButton(b);
        }
    }

    public void OnClickPlacementSlot(int index)
    {
        _characterTagSlot[index].ChangeCharacter(_placementSlot[index].Character);

        OnClickTagSlot();
    }

    public void OnClickTagSlot()
    {
        for (int i = 0; i < MAX_PLACEMENT_SLOT_COUNT; ++i)
        {
            _characterTagSlot[i].SetIndex();
        }
    }
}
