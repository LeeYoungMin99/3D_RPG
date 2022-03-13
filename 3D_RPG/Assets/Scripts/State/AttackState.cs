using System.Collections;
using UnityEngine;

public abstract class AttackState : State
{
    [SerializeField] protected float _delay = 0.2f;

    protected CharacterRotator _rotator;
    protected TargetManager _targetManager;
    protected Status _status;

    protected virtual void Start()
    {
        _rotator = GetComponent<CharacterRotator>();
        _targetManager = transform.parent.GetComponent<TargetManager>();
        _status = GetComponent<Status>();
    }

    public abstract IEnumerator Attack();
}
