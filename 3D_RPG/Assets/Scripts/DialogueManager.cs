using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager
{
    public static readonly DialogueManager Instance = new DialogueManager();

    private GameObject _dialogueWindow;
    private Canvas _UICanvas;
    private Text _dialogue;

    private bool _hasDialogWindow = false;

    public void ShowDialogue(string name, string dialog)
    {
        if (false == _hasDialogWindow)
        {
            _hasDialogWindow = true;

            Transform field = GameObject.Find("Field").transform;
            _dialogueWindow = field.Find("Dialogue Canvas").Find("Dialogue Window").gameObject;
            _dialogue = _dialogueWindow.transform.Find("Dialogue").GetComponent<Text>();
            _UICanvas = field.Find("Canvas").GetComponent<Canvas>();
        }

        _dialogueWindow.SetActive(true);
        _UICanvas.enabled = false;

        _dialogue.text = $"{name}\n\n{dialog}";
    }

    public void HideDialogue()
    {
        _dialogueWindow.SetActive(false);
        _UICanvas.enabled = true;
    }
}