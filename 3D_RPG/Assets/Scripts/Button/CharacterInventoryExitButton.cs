using UnityEngine;
using UnityEngine.UI;

public class CharacterInventoryExitButton : MonoBehaviour
{
    [SerializeField] private GameObject _field;
    [SerializeField] private GameObject _characterInventory;

    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        _characterInventory.SetActive(false);

        _field.SetActive(true);
    }
}