using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;

    private bool _isAuto = false;
    private AutoButtonEventArgs _autoButtonEventArgs = new AutoButtonEventArgs();
    private Transform _autoMoveTarget;
    public event EventHandler<AutoButtonEventArgs> OnAutoButtonClickEvent;

    public Transform AutoMoveTarget
    {
        get { return _autoMoveTarget; }
        set
        {
            _autoMoveTarget = value;

            _autoButtonEventArgs.AutoMoveTarget = _autoMoveTarget;

            OnAutoButtonClickEvent?.Invoke(this, _autoButtonEventArgs);
        }
    }

    public bool IsAuto { get { return _isAuto; } }

    private void Awake()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _isAuto = !_isAuto;

        _autoButtonEventArgs.CurAuto = _isAuto;
        _autoButtonEventArgs.AutoMoveTarget = AutoMoveTarget;

        OnAutoButtonClickEvent?.Invoke(this, _autoButtonEventArgs);

        if (true == _isAuto)
        {
            _text.text = "Auto\nOn";
        }
        else
        {
            _text.text = "Auto\nOff";
        }
    }
}
