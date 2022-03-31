using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] protected GameObject _cinemachineVirtualCamera;

    protected TalkEventArgs _talkEventArgs = new TalkEventArgs();
    protected Coroutine _talkCoroutine;

    public EventHandler<TalkEventArgs> TalkEvent;

    private const float SEARCH_RADIUS = 5f;
    protected const string TALK = "Talk";

    private static readonly Collider[] TARGET_COLLIDERS = new Collider[1];
    private static readonly LayerMask TARGET_LAYER = 1 << 6;

    protected virtual void Awake()
    {
        _talkEventArgs.ID = ID;

        TalkEvent -= QuestManager.Instance.EvaluateQuestTalkGoal;
        TalkEvent += QuestManager.Instance.EvaluateQuestTalkGoal;
    }

    private void Update()
    {
        if (false == Input.GetButtonDown(TALK)) return;

        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, SEARCH_RADIUS, TARGET_COLLIDERS, TARGET_LAYER);

        if (0 == targetCount) return;

        if (null != _talkCoroutine) return;

        _cinemachineVirtualCamera.SetActive(true);
        _talkCoroutine = StartCoroutine(Talk());
    }

    protected abstract IEnumerator Talk();
}
