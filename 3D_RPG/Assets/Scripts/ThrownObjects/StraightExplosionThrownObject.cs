using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightExplosionThrownObject : ThrownObject
{
    [SerializeField] private float _targetTime = 1f;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private int _targetCount = 16;

    private float _elapsedTime = 0f;
    private Collider[] _targetColliders;
    private LayerMask _targetMask;

    private void OnEnable()
    {
        _elapsedTime = 0f;

        EnableFlyingEffect();

        gameObject.layer = Owner.gameObject.layer;
        _targetMask = Target.gameObject.layer;
    }

    private void Start()
    {
        _targetColliders = new Collider[_targetCount];
    }

    private void FixedUpdate()
    {
        if (_elapsedTime >= _targetTime) return;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _targetTime)
        {
            EnableExplosionEffect();
        }

        float normalizedElapsedTime = Mathf.Clamp01(_elapsedTime / _targetTime);

        Vector3 targetPosition = Target.position + CORRECT_TARGET_POSITION_VECTOR;

        Vector3 curPosition = Owner.position + ((targetPosition - Owner.position) * normalizedElapsedTime);

        transform.position = curPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _targetColliders, _targetMask);

        if (other.gameObject == Target.gameObject)
        {
            StartCoroutine(DisableObjectAfterDuration());

            for (int i = 0; i < targetCount; ++i)
            {
                Debug.Log(_targetColliders[i].gameObject);
                _targetColliders[i].GetComponent<Status>().TakeDamage(Damage);
            }
        }
    }
}
