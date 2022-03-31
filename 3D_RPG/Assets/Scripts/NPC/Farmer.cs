using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : NPC
{
    public string[] Dialogues { get; set; } = new string[6];
    public int CurDialogueCount { get; set; } = 0;

    protected override IEnumerator Talk()
    {
        Time.timeScale = 0f;

        int index = 0;

        while (index < CurDialogueCount)
        {
            if (true == Input.GetButtonDown(TALK))
            {
                DialogueManager.Instance.ShowDialogue(gameObject.name, Dialogues[index]);

                ++index;
            }

            yield return null;
        }

        while (false == Input.GetButtonDown(TALK)) yield return null;

        Time.timeScale = 1f;
        _cinemachineVirtualCamera.SetActive(false);
        DialogueManager.Instance.HideDialogue();
        _talkCoroutine = null;

        TalkEvent?.Invoke(this, _talkEventArgs);
    }
}
