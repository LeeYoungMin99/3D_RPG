using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private PlacementSlot[] _deploymentSlot = new PlacementSlot[3];
    [SerializeField] private CharacterSlot[] _inventorySlot = new CharacterSlot[10];
    [SerializeField] private int _maxInventorySize = 20;
    [SerializeField] private Canvas _canvas;

    private int _curCharacterCount = 0;
    private int _curInventoryPage = 1;

    private Button[] _deploymentSlotButtons = new Button[3];

    public List<CharacterData> CharacterDatas = new List<CharacterData>();

    public CharacterData _curSelectCharacterData { get; set; }

    private void Awake()
    {
        _curInventoryPage = 1;

        ObtainCharacter(new CharacterData("Sword Man", 1, 1, 1));
        ObtainCharacter(new CharacterData("Archer", 1, 1, 1));
        ObtainCharacter(new CharacterData("Gunner", 1, 1, 1));
        ObtainCharacter(new CharacterData("Magician", 1, 1, 1));

        for (int i = 0; i < 3; ++i)
        {
            _deploymentSlotButtons[i] = _deploymentSlot[i].GetComponent<Button>();
        }
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

    public void ObtainCharacter(CharacterData data)
    {
        CharacterDatas.Add(data);
    }

    public void WithdrawCharacter(CharacterData data)
    {
        for (int i = 0; i < 3; ++i)
        {
            if(true == _deploymentSlot[i].CheckCharacterInfo(data))
            {
                _deploymentSlot[i].WithdrawCharacter();

                break;
            }
        }
    }

    public void SetInteractableDeploymentSlots(bool b)
    {
        for (int i = 0; i < 3; ++i)
        {
            _deploymentSlot[i].SlotButton.interactable = b;
        }
    }

    private void RefreshInventory()
    {
        int dataIndex = (_curInventoryPage - 1) * 10;
        int maxIndex = _curInventoryPage * 10;
        _curCharacterCount = CharacterDatas.Count;

        for (int slotIndex = 0; maxIndex > dataIndex; ++dataIndex, ++slotIndex)
        {
            if (dataIndex < _curCharacterCount)
            {
                _inventorySlot[slotIndex].CharacterInfo = CharacterDatas[dataIndex];
            }
            else
            {
                _inventorySlot[slotIndex].CharacterInfo = null;
            }
        }
    }
}
