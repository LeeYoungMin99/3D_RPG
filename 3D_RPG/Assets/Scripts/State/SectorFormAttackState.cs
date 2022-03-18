using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackState : AttackState
{
    [Range(0f, 180f)]
    [SerializeField] private float _hitAngle = 0f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private int _targetCount = 16;
    [SerializeField] private LayerMask _targetMask;

    private Collider[] _targetColliders;

    protected override void Start()
    {
        base.Start();

        _targetColliders = new Collider[_targetCount];
    }

    protected override IEnumerator Attack()
    {
        if (_delay > 0f) yield return new WaitForSeconds(_delay);

        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _targetColliders, _targetMask);

        Vector3 targetPosition;
        Vector3 myPosition = transform.position;
        myPosition.y = 0f;

        for (int i = 0; i < targetCount; ++i)
        {
            targetPosition = _targetColliders[i].transform.position;
            targetPosition.y = 0f;

            float angle = Mathf.Abs(CalculateAngle(myPosition, targetPosition));

            if (angle <= _hitAngle)
            {
                _targetColliders[i].GetComponent<Status>().TakeDamage(_status.ATK);
            }
        }
    }
}
