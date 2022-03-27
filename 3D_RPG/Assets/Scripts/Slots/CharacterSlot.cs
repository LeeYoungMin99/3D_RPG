using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterSlot : MonoBehaviour
{
    [SerializeField] protected CharacterInventorySlotManager _characterInventorySlotManager;
    [SerializeField] protected Button _slotButton;
    [SerializeField] protected Image _image;

    protected CharacterData _characterData;
    protected OnSlotClickEventArgs _eventArgs;

    protected virtual void Awake()
    {
        _slotButton.onClick.RemoveListener(OnClick);
        _slotButton.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
