using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AttackState
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private int _targetCount = 16;
    [SerializeField] private ParticleSystem _effect;

    private Collider[] _targetColliders;
    private GameObject _effectObject;

    protected override void Start()
    {
        base.Start();

        _targetColliders = new Collider[_targetCount];

        _effectObject = Instantiate(_effect.gameObject, transform.position, transform.rotation);
        _effect = _effectObject.GetComponent<ParticleSystem>();
    }

    public override void EnterState()
    {
        if (null == _targetManager.EnemyTarget)
        {
            return;
        }

        StartCoroutine(Attack());
    }

    protected override IEnumerator Attack()
    {
        Vector3 target = _targetManager.EnemyTarget.position;

        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        _effect.transform.position = target;
        _effect.Play();

        int targetCount = Physics.OverlapSphereNonAlloc(target, _radius, _targetColliders, _targetMask);

        for (int i = 0; i < targetCount; ++i)
        {
            _targetColliders[i].GetComponent<CharacterStatus>().TakeDamage(_status.ATK);
        }
    }
}
