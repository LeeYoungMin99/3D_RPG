using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventorySlotManager : MonoBehaviour
{
    [SerializeField] private const int MAX_SHOW_INVENTORY_SLOT = 10;
    [SerializeField] private const int MAX_PLACEMENT_SLOT = 3;

    [SerializeField] private PlacementSlot[] _placementSlot = new PlacementSlot[MAX_PLACEMENT_SLOT];
    [SerializeField] private CharacterSlot[] _inventorySlot = new CharacterSlot[MAX_SHOW_INVENTORY_SLOT];
    [SerializeField] private Canvas _canvas;
    [SerializeField] private int _maxInventorySize = 20;

    private int _curCharacterCount = 0;
    private int _curInventoryPage = 1;

    private List<Character> _characterList = new List<Character>();

    public Character CurSelectCharacter;

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

    public void ObtainCharacter(Character data)
    {
        _characterList.Add(data);
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
        for (int i = 0; i < MAX_PLACEMENT_SLOT; ++i)
        {
            _placementSlot[i].SetInteractabletSlotButton(b);
        }
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
}
