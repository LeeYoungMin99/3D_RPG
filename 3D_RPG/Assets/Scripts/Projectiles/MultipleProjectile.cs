using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleProjectile : StraightProjectile
{
    [SerializeField] private float _intervalAngle = 5f;

    private void OnTriggerEnter(Collider other)
    {
        SetTrailRendererDisplay(false);
        SetFlyingEffectDisplay(false);
        SetExplosionEffectDisplay(true);

        other.GetComponent<CharacterStatus>().TakeDamage(Damage, GainExperience);

        StartCoroutine(CinemachineShaker.Instance.ShakeCamera(_amplitueGain, _shakeTime));
    }

    protected override void OnEnable()
    {
        SetTrailRendererDisplay(true);
        SetFlyingEffectDisplay(true);
        SetExplosionEffectDisplay(false);

        _startPosition = StartPosition.position;
        transform.position = StartPosition.position;
        _isMove = true;

        float angle = (transform.parent.childCount - 1) * -_intervalAngle / 2 + (_intervalAngle * transform.GetSiblingIndex());

        transform.LookAt(Target);

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + angle, 0f);
    }

    protected override void GetOutOfTheMaximumRange()
    {
        SetTrailRendererDisplay(false);
        SetFlyingEffectDisplay(false);
        SetExplosionEffectDisplay(true);
    }
}
