using System.Collections;
using UnityEngine;

public abstract class ThrownObject : MonoBehaviour
{
    [Header("Particle")]
    [Tooltip("If there is no effect, there is no need to add it.")]
    [SerializeField] protected ParticleSystem _flyingEffect;
    [Tooltip("If there is no effect, there is no need to add it.")]
    [SerializeField] protected ParticleSystem _explosionEffect;
    [Space(5)]
    [SerializeField] protected Collider _collider;

    protected float _explosionEffectDuration = 0f;
    protected bool _hasFlyingEffect = false;
    protected bool _hasExplosionEffect = false;

    public Transform Owner { protected get; set; }
    public Transform Target { protected get; set; }
    public LayerMask TargetLayer { protected get; set; }
    public float Damage { protected get; set; }

    protected static readonly Vector3 CORRECT_TARGET_POSITION_VECTOR = new Vector3(0f, 1f, 0f);

    private void Awake()
    {
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

    protected IEnumerator DisableObjectAfterDuration()
    {
        yield return new WaitForEndOfFrame();

        _collider.enabled = false;

        if (0f != _explosionEffectDuration)
        {
            yield return new WaitForSeconds(_explosionEffectDuration);
        }

        gameObject.SetActive(false);
        _collider.enabled = true;
    }

    protected void EnableFlyingEffect()
    {
        if (true == _hasFlyingEffect)
        {
            _flyingEffect.gameObject.SetActive(true);
        }

        if (true == _hasExplosionEffect)
        {
            _explosionEffect.gameObject.SetActive(false);
        }
    }

    protected void EnableExplosionEffect()
    {
        if (true == _hasFlyingEffect)
        {
            _flyingEffect.gameObject.SetActive(false);
        }

        if (true == _hasExplosionEffect)
        {
            _explosionEffect.gameObject.SetActive(true);
        }

        StartCoroutine(DisableObjectAfterDuration());
    }
}
