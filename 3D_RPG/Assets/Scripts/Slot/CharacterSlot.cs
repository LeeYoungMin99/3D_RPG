using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] protected CharacterInventorySlotManager _characterInventorySlotManager;
    [SerializeField] protected Button _slotButton;
    [SerializeField] protected Image _image;
    [SerializeField] protected Text _text;

    protected CharacterData _character;
    protected int _index;

    private void Awake()
    {
        _slotButton.onClick.RemoveListener(OnClick);
        _slotButton.onClick.AddListener(OnClick);
        _index = transform.GetSiblingIndex();
    }

    public void SetInteractabletSlotButton(bool b)
    {
        _slotButton.interactable = b;
    }

    public virtual void ChangeCharacter(CharacterData character) { }

    protected virtual void OnClick() { }
}
