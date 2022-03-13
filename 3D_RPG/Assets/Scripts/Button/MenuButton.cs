using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private Button _button;

    private bool _isShow = true;

    private void Start()
    {
        HideList();

        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        HideList();
    }

    void OnClick()
    {
        if(true == _isShow)
        {
            HideList();
        }
        else
        {
            ShowList();
        }
    }

    public void ShowList()
    {
        _isShow = true;

        foreach (Button button in _buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void HideList()
    {
        _isShow = false;

        foreach (Button button in _buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
