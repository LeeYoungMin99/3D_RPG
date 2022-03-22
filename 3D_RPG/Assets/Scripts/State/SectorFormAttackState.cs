using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackState : AttackState
{
    [Range(0f, 180f)]
    [SerializeField] private int _hitAngle = 0;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private int _targetCount = 16;
    [SerializeField] private LayerMask _targetMask;

    [Header("Arc Line Render")]
    [SerializeField] private bool _hasArcLineRenderer = false;
    [SerializeField] private GameObject _arcLineRenderer;

    private Collider[] _targetColliders;

    protected override void Start()
    {
        base.Start();

        _targetColliders = new Collider[_targetCount];

        _arcLineRenderer = Instantiate(_arcLineRenderer, transform.position + new Vector3(0f, 0.3f, 0f), transform.rotation);
        _arcLineRenderer.transform.SetParent(gameObject.transform);

        _arcLineRenderer.GetComponent<ArcLineRenderer>().Angle = _hitAngle;
        _arcLineRenderer.transform.localScale *= _radius;
    }

    protected override IEnumerator Attack()
    {
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        _arcLineRenderer.SetActive(true);

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
                _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(_status.ATK);
            }
        }
    }

    public override void ExitState()
    {
        _arcLineRenderer.SetActive(false);
    }
}
