using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AttackState
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private int _targetCount = 16;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private int _objectPoolCount = 1;

    private Collider[] _targetColliders;
    private GameObject[] _effectPool;
    private LayerMask _targetMask;
    private int _curIndex = 0;

    private static readonly Vector3 CORRECT_TARGET_POSITION_VECTOR = new Vector3(0f,0.3f,0f);

    protected override void Awake()
    {
        base.Awake();

        _targetColliders = new Collider[_targetCount];

        _effectPool = new GameObject[_objectPoolCount];

        _targetMask = _targetManager.EnemyTargetLayer;

        for (int i = 0; i < _objectPoolCount; ++i)
        {
            _effectPool[i] = Instantiate(_effectPrefab, transform.position, transform.rotation);
        }
    }

    public override void EnterState()
    {
        if (null == _targetManager.EnemyTarget) return;

        StartCoroutine(Attack());
    }

    protected override IEnumerator Attack()
    {
        Vector3 target = _targetManager.EnemyTarget.position;

        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        _effectPool[_curIndex].transform.position = target + CORRECT_TARGET_POSITION_VECTOR;
        _effectPool[_curIndex].SetActive(true);

        ++_curIndex;

        if(_objectPoolCount <= _curIndex)
        {
            _curIndex = 0;
        }

        int targetCount = Physics.OverlapSphereNonAlloc(target, _radius, _targetColliders, _targetMask);

        for (int i = 0; i < targetCount; ++i)
        {
            _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(_status.ATK, GainExperience);
        }
    }
}
