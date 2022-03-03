using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] protected CharacterInventorySlotManager _characterInventorySlotManager;
    [SerializeField] protected Image _image;
    [SerializeField] protected Button _slotButton;
    [SerializeField] protected Text _text;

    public Character Character { get; protected set; }

    private void Awake()
    {
        _slotButton.onClick.RemoveListener(OnClick);
        _slotButton.onClick.AddListener(OnClick);
    }

    public void SetInteractabletSlotButton(bool b)
    {
        _slotButton.interactable = b;
    }

    public void ChangeCharacter(Character character)
    {
        Character = character;
    }

    protected virtual void OnClick() { }
}
