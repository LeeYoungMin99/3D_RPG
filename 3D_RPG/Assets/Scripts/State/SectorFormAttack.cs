using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttack : Attack
{
    [Range(0f, 180f)]
    [SerializeField] private float _horizontalAngle = 0f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _delay = 0.2f;
    [SerializeField] private LayerMask _targetMask;

    private PlayerRotator _rotator;
    private TargetManager _targetManager;
    private Vector3 _forward;
    private readonly Collider[] _targetColliders = new Collider[16];

    private void Start()
    {
        _rotator = GetComponent<PlayerRotator>();
        _targetManager = transform.parent.GetComponent<TargetManager>();
    }

    private IEnumerator GiveDamage()
    {
        yield return new WaitForSeconds(_delay);

        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _targetColliders, _targetMask);

        Vector3 targetDir;

        for (int i = 0; i < targetCount; ++i)
        {
            Vector3 targetPosition = _targetColliders[i].transform.position;
            targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);

            Vector3 myPosition = new Vector3(transform.position.x, 0f, transform.position.z);

            targetDir = (targetPosition - myPosition).normalized;

            float dot = Vector3.Dot(targetDir, _forward);

            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= _horizontalAngle)
            {
                Debug.Log($"데미지를 입히다");
            }
        }
    }

    private IEnumerator GetLookDir()
    {
        yield return new WaitForEndOfFrame();

        _forward = transform.forward;
    }

    public override void EnterState()
    {
        StartCoroutine(GiveDamage());
        StartCoroutine(InitLocalPosition());

        if (null != _targetManager.Target)
        {
            StartCoroutine(_rotator.LookatTarget(_targetManager.Target));
        }
        else
        {
            StartCoroutine(_rotator.RotateToTargetRotation());
        }

        StartCoroutine(GetLookDir());
    }

    public override void ExitState()
    {
        StartCoroutine(InitLocalPosition());
        StartCoroutine(_rotator.RotateToTargetRotation());
    }
}
