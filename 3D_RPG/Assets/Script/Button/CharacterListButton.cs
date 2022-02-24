using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListButton : MonoBehaviour
{
    [SerializeField] private GameObject _field;
    [SerializeField] private GameObject _characterListWindow;

    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        _field.SetActive(false);

        _characterListWindow.SetActive(true);
    }
}
