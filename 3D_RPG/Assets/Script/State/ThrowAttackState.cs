using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttackState : AttackState
{
    [SerializeField] private ThrownObject _thrownObjectPrefab;
    [SerializeField] private float _thrownObjectSpeed = 20f;
    [SerializeField] private Transform _startPosition;

    private const int OBJECT_COUNT = 4;
    private GameObject[] _thrownObjectPool = new GameObject[OBJECT_COUNT];
    private ThrownObject[] _thrownObjects = new ThrownObject[OBJECT_COUNT];
    private int _curIndex = 0;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < OBJECT_COUNT; ++i)
        {
            _thrownObjectPool[i] = Instantiate(_thrownObjectPrefab.gameObject, _startPosition.position, transform.rotation);
            _thrownObjects[i] = _thrownObjectPool[i].GetComponent<ThrownObject>();
            _thrownObjects[i].SetSpeed(_thrownObjectSpeed);
            _thrownObjectPool[i].SetActive(false);
        }
    }

    public override void EnterState()
    {
        StartCoroutine(InitializeLocalPositionAtEndOfFrame());

        if (null != _targetManager.Target)
        {
            StartCoroutine(_rotator.LookAtTargetAtEndOfFrame(_targetManager.Target));
        }
        else
        {
            return;
        }

        StartCoroutine(Attack());

        ++_curIndex;

        if (_curIndex >= OBJECT_COUNT)
        {
            _curIndex = 0;
        }
    }

    public override void ExitState()
    {
        StartCoroutine(_rotator.RotateToTargetRotationAtEndOfFrame());
    }

    public override IEnumerator Attack()
    {
        int index = _curIndex;

        if (_delay > 0f)
        {
            yield return new WaitForSeconds(_delay);
        }

        _thrownObjectPool[index].transform.position = _startPosition.position;
        _thrownObjects[index].SetTarget(_targetManager.Target);

        _thrownObjectPool[index].SetActive(true);
    }
}
