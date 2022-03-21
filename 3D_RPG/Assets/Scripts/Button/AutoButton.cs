using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;

    private bool _isAuto = false;

    public event EventHandler<AutoButtonEventArgs> OnClickEvent;

    public bool IsAuto { get { return _isAuto; } }

    public class AutoButtonEventArgs : EventArgs
    {
        public bool CurAuto;

        public AutoButtonEventArgs(bool b)
        {
            CurAuto = b;
        }
    }

    private void Awake()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _isAuto = !_isAuto;

        OnClickEvent?.Invoke(this, new AutoButtonEventArgs(_isAuto));

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
