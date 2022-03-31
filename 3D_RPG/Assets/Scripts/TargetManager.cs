using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    protected LayerMask _enemyTargetLayer = 1 << 6;

    public Transform Target;

    protected static readonly Collider[] TARGET_COLLIDERS = new Collider[16];

    protected const float SEARCH_DELAY = 0.25f;
    protected const float EMEMY_SEARCH_RADIUS = 15f;

    public Transform EnemyTarget { get; protected set; }
    public LayerMask EnemyTargetLayer { get { return _enemyTargetLayer; } }

    protected virtual void OnEnable()
    {
        StartCoroutine(SearchTarget());
    }

    protected virtual IEnumerator SearchTarget()
    {
        while (true == enabled)
        {
            EnemyTarget = SearchTargetHelper(EMEMY_SEARCH_RADIUS, _enemyTargetLayer);

            yield return new WaitForSeconds(SEARCH_DELAY);
        }
    }

    protected Transform SearchTargetHelper(float searchRadius, LayerMask targetLayer)
    {
        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, TARGET_COLLIDERS, targetLayer);

        float minDistance = float.MaxValue;
        float distance;

        Transform target = null;

        if (0 != targetCount)
        {
            for (int i = 0; i < targetCount; ++i)
            {
                distance = Vector3.Distance(TARGET_COLLIDERS[i].transform.position, transform.position);

                if (minDistance <= distance) continue;

                minDistance = distance;

                target = TARGET_COLLIDERS[i].transform;
            }
        }

        return target;
    }
}
