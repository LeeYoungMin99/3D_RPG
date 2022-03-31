using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IExperienceGainer
{
    [Header("Particle")]
    [Tooltip("If there is no effect, there is no need to add it.")]
    [SerializeField] protected TrailRenderer _trailRenderer;
    [Tooltip("If there is no effect, there is no need to add it.")]
    [SerializeField] protected ParticleSystem _flyingEffect;
    [Tooltip("If there is no effect, there is no need to add it.")]
    [SerializeField] protected ParticleSystem _explosionEffect;
    [Space(5)]
    [SerializeField] protected Collider _collider;

    protected float _explosionEffectDuration = 0f;
    protected bool _hasTrailRenderer = false;
    protected bool _hasFlyingEffect = false;
    protected bool _hasExplosionEffect = false;

    public GameObject Owner { protected get; set; }
    public Transform StartPosition { protected get; set; }
    public Transform Target { protected get; set; }
    public LayerMask TargetLayer { protected get; set; }
    public float Damage { protected get; set; }

    protected static readonly Vector3 CORRECT_TARGET_POSITION_VECTOR = new Vector3(0f, 1f, 0f);

    protected virtual void Awake()
    {
        if (null != _trailRenderer)
        {
            _hasTrailRenderer = true;
        }

        if (null != _flyingEffect)
        {
            _hasFlyingEffect = true;
            _explosionEffectDuration = _explosionEffect.main.duration;
        }

        if (null != _explosionEffect)
        {
            _hasExplosionEffect = true;
            _explosionEffectDuration = _explosionEffect.main.duration;
        }
    }

    protected virtual void OnEnable()
    {
        SetTrailRendererDisplay(true);
        SetFlyingEffectDisplay(true);
        SetExplosionEffectDisplay(false);

        transform.position = StartPosition.position;
    }

    protected IEnumerator DisableObjectAfterDuration()
    {
        yield return new WaitForEndOfFrame();

        _collider.enabled = false;

        if (0f != _explosionEffectDuration) yield return new WaitForSeconds(_explosionEffectDuration);

        gameObject.SetActive(false);
        _collider.enabled = true;
    }

    protected void SetTrailRendererDisplay(bool b)
    {
        if (false == _hasTrailRenderer) return;

        _trailRenderer.enabled = b;
    }

    protected void SetFlyingEffectDisplay(bool b)
    {
        if (false == _hasFlyingEffect) return;

        _flyingEffect.gameObject.SetActive(b);
    }

    protected void SetExplosionEffectDisplay(bool b)
    {
        if (true == _hasExplosionEffect)
        {
            _explosionEffect.gameObject.SetActive(b);
        }

        if (false == b) return;

        if (false == _hasExplosionEffect)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(DisableObjectAfterDuration());
        }
    }

    public void GainExperience(object sender, DeathEventArgs args)
    {
        Owner.GetComponent<CharacterStatus>().ObtainExperience(args.Experience);
    }
}
