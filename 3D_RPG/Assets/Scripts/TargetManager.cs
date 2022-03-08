using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] float _searchDelay = 0.25f;
    [SerializeField] float _radius = 6f;
    [SerializeField] LayerMask _targetMask;

    private readonly Collider[] _targetColliders = new Collider[16];
    public Transform Target { get; private set; }

    private void OnEnable()
    {
        StartCoroutine(SearchTarget());
    }

    private void OnDisable()
    {
        StopCoroutine(SearchTarget());
    }

    private IEnumerator SearchTarget()
    {
        while (true == enabled)
        {
            int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _targetColliders, _targetMask);

            float minDistance = 500f;
            float distance = 0;

            if (0 != targetCount)
            {
                for (int i = 0; i < targetCount; ++i)
                {
                    distance = Vector3.Distance(_targetColliders[i].transform.position, transform.position);

                    if (minDistance > distance)
                    {
                        minDistance = distance;

                        Target = _targetColliders[i].transform;
                    }
                }
            }
            else
            {
                Target = null;
            }

            yield return new WaitForSeconds(_searchDelay);
        }
    }
}
