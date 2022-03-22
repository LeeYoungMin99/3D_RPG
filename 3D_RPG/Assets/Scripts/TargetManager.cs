using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [Header("Target Enemy Setting")]
    [SerializeField] protected float _searchDelay = 0.25f;
    [SerializeField] protected float _enemySearchRadius = 15f;
    [SerializeField] protected LayerMask _enemyTargetLayer;

    protected readonly Collider[] _targetColliders = new Collider[16];
    protected Coroutine _coroutineSearchTarget;

    public Transform Target;

    public Transform EnemyTarget { get; protected set; }
    public LayerMask EnemyTargetLayer { get { return _enemyTargetLayer; } }

    private void OnDisable()
    {
        StopCoroutine(_coroutineSearchTarget);
    }

    protected virtual void OnEnable()
    {
        _coroutineSearchTarget = StartCoroutine(SearchTarget());
    }

    protected virtual IEnumerator SearchTarget()
    {
        while (true == enabled)
        {
            EnemyTarget = SearchTargetHelper(_enemySearchRadius, _enemyTargetLayer);

            yield return new WaitForSeconds(_searchDelay);
        }
    }

    protected Transform SearchTargetHelper(float searchRadius, LayerMask targetLayer)
    {
        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, _targetColliders, targetLayer);

        float minDistance = float.MaxValue;
        float distance;

        Transform target = null;

        if (0 != targetCount)
        {
            for (int i = 0; i < targetCount; ++i)
            {
                distance = Vector3.Distance(_targetColliders[i].transform.position, transform.position);

                if (minDistance > distance)
                {
                    minDistance = distance;

                    target = _targetColliders[i].transform;
                }
            }
        }

        return target;
    }
}
