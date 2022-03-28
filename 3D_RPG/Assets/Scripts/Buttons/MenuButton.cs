using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private Button _button;

    private bool _isShowing = true;

    private void Awake()
    {
        HideList();

        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (true == _isShowing)
        {
            HideList();
        }
        else
        {
            ShowList();
        }
    }

    private void ShowList()
    {
        _isShowing = true;

        foreach (Button button in _buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void HideList()
    {
        _isShowing = false;

        foreach (Button button in _buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
