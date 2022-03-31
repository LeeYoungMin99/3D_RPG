using UnityEngine;
using UnityEngine.UI;

public class CharacterInventoryExitButton : MonoBehaviour
{
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private GameObject _characterInventory;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Time.timeScale = 1f;

        _characterInventory.SetActive(false);

        _playerCamera.SetActive(true);
        _canvas.enabled = true;
    }
}
