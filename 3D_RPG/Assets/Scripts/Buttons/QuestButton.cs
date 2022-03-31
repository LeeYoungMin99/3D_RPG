using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] GameObject _questWindow;
    [SerializeField] Text _questNameText;
    [SerializeField] Text _questDescriptionText;
    [SerializeField] Text _questProgressText;

    private void Awake()
    {
        _button.onClick.RemoveListener(OnClick);
        _button.onClick.AddListener(OnClick);

        QuestManager.Instance.OnQuestChangeEvent -= SetQuestText;
        QuestManager.Instance.OnQuestChangeEvent += SetQuestText;

        gameObject.SetActive(false);
    }

    private void OnClick()
    {
        _questWindow.SetActive(!_questWindow.activeSelf);
    }

    private void SetQuestText(object sender, QuestChangeEventArgs args)
    {
        _questNameText.text = args.QuestName;
        _questDescriptionText.text = args.QuestDescription;
    }
}
