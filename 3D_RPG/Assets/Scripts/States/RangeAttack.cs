using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AttackState
{
    [SerializeField] protected float _hitRadius = 1f;
    [SerializeField] protected int _targetCount = 16;
    [SerializeField] protected GameObject _effectPrefab;
    [SerializeField] protected int _objectPoolCount = 1;

    protected Collider[] _targetColliders;
    protected GameObject[] _effectPool;
    protected LayerMask _targetLayer;
    protected int _curIndex = 0;

    protected static readonly Vector3 CORRECT_TARGET_POSITION_VECTOR = new Vector3(0f, 0.3f, 0f);

    protected override void Awake()
    {
        base.Awake();

        _targetColliders = new Collider[_targetCount];

        _effectPool = new GameObject[_objectPoolCount];

        _targetLayer = _targetManager.EnemyTargetLayer;

        Transform field = GameObject.Find("Field").transform;

        for (int i = 0; i < _objectPoolCount; ++i)
        {
            _effectPool[i] = Instantiate(_effectPrefab, transform.position, transform.rotation, field);
            _effectPool[i].transform.localScale *= _hitRadius;
        }
    }

    protected override IEnumerator Attack()
    {
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        Vector3 target = _targetManager.EnemyTarget.position;

        AttackHelper(target);

        CalculateObjectPoolIndex();
    }

    protected void CalculateObjectPoolIndex()
    {
        ++_curIndex;

        if (_objectPoolCount <= _curIndex)
        {
            _curIndex = 0;
        }
    }

    protected void AttackHelper(Vector3 targetPosition)
    {
        _effectPool[_curIndex].transform.position = targetPosition + CORRECT_TARGET_POSITION_VECTOR;
        _effectPool[_curIndex].SetActive(true);

        int targetCount = Physics.OverlapSphereNonAlloc(targetPosition, _hitRadius, _targetColliders, _targetLayer);

        StartCoroutine(CinemachineShaker.Instance.ShakeCamera(_amplitueGain, _shakeTime));

        if (0 == targetCount) return;

        for (int i = 0; i < targetCount; ++i)
        {
            _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(_status.ATK, GainExperience);
        }
    }
}
