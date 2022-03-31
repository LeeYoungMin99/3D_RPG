using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        foreach (Button button in _buttons)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }
}
