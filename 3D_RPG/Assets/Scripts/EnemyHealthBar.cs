using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _nameText;
    [SerializeField] private GameObject _healthBarBackground;
    [SerializeField] private GameObject _textBackground;

    private bool _hasTarget = false;
    private CharacterStatus _tagetStatus;

    public PlayerTargetManager PlayerTargetManager { private get; set; }

    private void LateUpdate()
    {
        if (true == _hasTarget)
        {
            _healthBar.value = _tagetStatus.CurHP / _tagetStatus.MaxHP;
            _levelText.text = $"Lv.{_tagetStatus.Level}";
            _nameText.text = $"{_tagetStatus.gameObject.name}";
        }
    }

    private bool CheckEnemyManagerTarget()
    {
        if (null == PlayerTargetManager.EnemyTarget)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SetUIDisplay(bool b)
    {
        _healthBarBackground.SetActive(b);
        _textBackground.SetActive(b);
    }

    public void RefreshTarget()
    {
        if (true == CheckEnemyManagerTarget())
        {
            _hasTarget = true;

            _tagetStatus = PlayerTargetManager.EnemyTarget.GetComponent<CharacterStatus>();

            SetUIDisplay(true);
        }
        else
        {
            _hasTarget = false;

            _tagetStatus = null;

            SetUIDisplay(false);
        }
    }
}
