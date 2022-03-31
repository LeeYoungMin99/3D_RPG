using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventoryButton : MonoBehaviour
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
        Time.timeScale = 0f;

        _playerCamera.SetActive(false);
        _canvas.enabled = false;

        _characterInventory.SetActive(true);
    }
}
