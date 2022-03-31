using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPiercingProjectile : StraightProjectile
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CharacterStatus>().TakeDamage(Damage, GainExperience);

        StartCoroutine(CinemachineShaker.Instance.ShakeCamera(_amplitueGain, _shakeTime));
    }

    protected override void GetOutOfTheMaximumRange()
    {
        SetTrailRendererDisplay(false);
        SetFlyingEffectDisplay(false);
        SetExplosionEffectDisplay(true);
    }
}
