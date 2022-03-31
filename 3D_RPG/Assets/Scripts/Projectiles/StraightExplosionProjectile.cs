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
        _isMove = false;

        SetTrailRendererDisplay(false);
        SetFlyingEffectDisplay(false);
        SetExplosionEffectDisplay(true);

        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _hitRadius, _targetColliders, TargetLayer);

        StartCoroutine(CinemachineShaker.Instance.ShakeCamera(_amplitueGain, _shakeTime));

        if (0 == targetCount) return;

        for (int i = 0; i < targetCount; ++i)
        {
            _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(Damage, GainExperience);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _targetColliders = new Collider[_targetCount];
    }

    protected override void GetOutOfTheMaximumRange()
    {
        Explosion();
    }
}
