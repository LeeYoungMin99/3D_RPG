using UnityEngine;
using UnityEngine.UI;

public class CharacterInventorySlot : MonoBehaviour
{
    [SerializeField] protected CharacterInventorySlotManager _characterInventorySlotManager;
    [SerializeField] protected Image _image;
    [SerializeField] protected Button _slotButton;
    [SerializeField] protected Text _text;

    protected Character _character;

    public void SetInteractabletSlotButton(bool b)
    {
        _slotButton.interactable = b;
    }

    public void ChangeCharacter(Character character)
    {
        _character = character;
    }
}
