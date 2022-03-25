using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightExplosionProjectile : StraightProjectile
{
    [SerializeField] private float _hitRadius = 2f;
    [SerializeField] private int _targetCount = 16;

    private Collider[] _targetColliders;

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }

    private void Explosion()
    {
        SetTrailRendererDisplay(false);
        SetFlyingEffectDisplay(false);
        SetExplosionEffectDisplay(true);

        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _hitRadius, _targetColliders, TargetLayer);

        if (0 == targetCount) return;

        for (int i = 0; i < targetCount; ++i)
        {
            _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(Damage);
        }
    }

    protected override void Start()
    {
        base.Start();

        _targetColliders = new Collider[_targetCount];
    }

    protected override void GetOutOfTheMaximumRange()
    {
        Explosion();
    }
}
