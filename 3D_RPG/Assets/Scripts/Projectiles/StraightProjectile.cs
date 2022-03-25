using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StraightProjectile : Projectile
{
    [SerializeField] protected float _maximumRange = 15f;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _speed = 15f;

    protected Vector3 _startPosition;

    private const float CORRECT_SPEED = 40f;

    protected virtual void Start()
    {
        _speed *= CORRECT_SPEED;

        _startPosition = transform.position;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _startPosition = transform.position;

        Vector3 targetPosition = Target.position + CORRECT_TARGET_POSITION_VECTOR;

        transform.LookAt(targetPosition);
    }

    private void FixedUpdate()
    {
        float distanceCurrentlyFlown = Vector3.Distance(_startPosition, transform.position);

        if (_maximumRange <= distanceCurrentlyFlown)
        {
            GetOutOfTheMaximumRange();
        }

        _rigidbody.velocity = transform.forward * (_speed * Time.deltaTime);
    }

    protected abstract void GetOutOfTheMaximumRange();
}
