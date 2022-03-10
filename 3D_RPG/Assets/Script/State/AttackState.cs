using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    [SerializeField] protected float _delay = 0.2f;

    protected PlayerRotator _rotator;
    protected TargetManager _targetManager;
    protected Status _status;

    protected virtual void Start()
    {
        _rotator = GetComponent<PlayerRotator>();
        _targetManager = transform.parent.GetComponent<TargetManager>();
        _status = GetComponent<Status>();
    }

    public abstract IEnumerator Attack();
}
