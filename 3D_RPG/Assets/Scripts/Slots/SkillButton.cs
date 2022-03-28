using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [Header("Skill Button Setting")]
    [SerializeField] private TagSlot _tagSlot;
    [SerializeField] private Button _tagSlotButton;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private CharacterData _characterData;
    private AttackState _characterSkill;
    private float _curCooldownTime;

    private void Awake()
    {
        _tagSlot.CharacterDataChangeEvent -= SubscribeSkill;
        _tagSlot.CharacterDataChangeEvent += SubscribeSkill;

        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (false == _characterData.PawnActive)
        {
            _tagSlotButton.onClick?.Invoke();
        }

        _characterSkill.UseSkill();
    }

    private void SubscribeSkill(object sender, SkillEventArgs args)
    {
        if (null != _characterSkill)
        {
            _characterSkill.UseSkillEvent -= StartCooldownHelper;
        }

        _button.interactable = true;

        _characterData = args.CharacterData;
        _characterSkill = args.CharacterData.Skill;

        _characterSkill.UseSkillEvent -= StartCooldownHelper;
        _characterSkill.UseSkillEvent += StartCooldownHelper;
    }

    private void StartCooldownHelper(object sender, SkillEventArgs args)
    {
        StartCoroutine(StartCooldown(args.CooldownTime));
    }

    private IEnumerator StartCooldown(float cooldownTime)
    {
        _button.interactable = false;

        _curCooldownTime = cooldownTime;

        while (0f < _curCooldownTime)
        {
            _curCooldownTime -= Time.deltaTime;

            _image.fillAmount = 1f - (_curCooldownTime / cooldownTime);

            yield return new WaitForFixedUpdate();
        }

        _image.fillAmount = 1f;

        _button.interactable = true;
    }
}
