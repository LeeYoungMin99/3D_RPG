using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventorySlot : MonoBehaviour
{
    public CharacterData CharacterInfo { get; set; }

    [SerializeField] protected CharacterInventory _characterInventory;

    [SerializeField] protected Image _image;
    public Button SlotButton { get; set; }
    protected string _path;
    protected virtual void Awake()
    {
        SlotButton = GetComponent<Button>();
    }
}
