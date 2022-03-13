using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private float _searchDelay = 0.25f;
    [SerializeField] private float _enemySearchRadius = 6f;
    [SerializeField] private float _NPCSearchRadius = 3f;
    [SerializeField] private LayerMask _enemyTargetMask;
    [SerializeField] private LayerMask _NPCTargetMask;

    private readonly Collider[] _targetColliders = new Collider[16];
    private Coroutine _coroutineSearchTarget;
    public Transform EnemyTarget { get; private set; }
    public Transform NPCTarget { get; private set; }
    public Transform Target;

    private void OnEnable()
    {
        _coroutineSearchTarget = StartCoroutine(SearchTarget());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutineSearchTarget);
    }

    private IEnumerator SearchTarget()
    {
        while (true == enabled)
        {
            int enemyTargetCount = Physics.OverlapSphereNonAlloc(transform.position, _enemySearchRadius, _targetColliders, _enemyTargetMask);

            float minDistance = 500f;
            float distance;

            if (0 != enemyTargetCount)
            {
                for (int i = 0; i < enemyTargetCount; ++i)
                {
                    distance = Vector3.Distance(_targetColliders[i].transform.position, transform.position);

                    if (minDistance > distance)
                    {
                        minDistance = distance;

                        EnemyTarget = _targetColliders[i].transform;
                    }
                }
            }
            else
            {
                EnemyTarget = null;
            }

            int NPCTargetCount = Physics.OverlapSphereNonAlloc(transform.position, _NPCSearchRadius, _targetColliders, _NPCTargetMask);

            if (0 != NPCTargetCount)
            {
                for (int i = 0; i < enemyTargetCount; ++i)
                {
                    distance = Vector3.Distance(_targetColliders[i].transform.position, transform.position);

                    if (minDistance > distance)
                    {
                        minDistance = distance;

                        NPCTarget = _targetColliders[i].transform;
                    }
                }
            }
            else
            {
                NPCTarget = null;
            }

            yield return new WaitForSeconds(_searchDelay);
        }
    }
}
