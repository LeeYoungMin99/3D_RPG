using System.Collections;
using UnityEngine;

public class ProjectileAttack : AttackState
{
    [SerializeField] private Projectile _projectileObjectPrefab;
    [SerializeField] private Transform _startPosition;
    [Range(1, 30)]
    [SerializeField] private int _maxShotCount = 1;
    [Range(1, 30)]
    [SerializeField] private int _countPerShot = 1;
    [Range(0f, 2f)]
    [SerializeField] private float _intervalTime = 0.5f;
    [Header("Damage Split")]
    [SerializeField] private bool _isSplit = true;

    private GameObject[] _projectileObjectPool;
    private Projectile[] _projectileObjects;
    private int _objectPoolCount;
    private int _curIndex = 0;
    private int _curShotCount = 0;

    private const int OBJECT_POOL_SIZE = 2;

    protected override void Awake()
    {
        base.Awake();

        _objectPoolCount = _maxShotCount * _countPerShot;

        _projectileObjectPool = new GameObject[_objectPoolCount * OBJECT_POOL_SIZE];
        _projectileObjects = new Projectile[_objectPoolCount * OBJECT_POOL_SIZE];

        int index;
        Transform field = GameObject.Find("Field").transform;
        for (int i = 0; i < OBJECT_POOL_SIZE; ++i)
        {
            GameObject thrownObjects = new GameObject("Projectile Objects");
            thrownObjects.transform.SetParent(field);

            for (int j = 0; j < _objectPoolCount; ++j)
            {
                index = i * _objectPoolCount + j;

                _projectileObjectPool[index] = Instantiate(_projectileObjectPrefab.gameObject, _startPosition.position, transform.rotation, thrownObjects.transform);
                _projectileObjects[index] = _projectileObjectPool[index].GetComponent<Projectile>();
                _projectileObjects[index].Owner = gameObject;
                _projectileObjects[index].StartPosition = _startPosition;
                _projectileObjects[index].TargetLayer = _targetManager.EnemyTargetLayer;
            }
        }

        _objectPoolCount *= OBJECT_POOL_SIZE;
    }

    protected override IEnumerator Attack()
    {
        Transform target = _targetManager.EnemyTarget;
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        StartCoroutine(Shot(target));
    }

    private IEnumerator Shot(Transform target)
    {
        while (_maxShotCount > _curShotCount)
        {
            for (int i = 0; i < _countPerShot; ++i)
            {
                _projectileObjectPool[_curIndex].transform.position = _startPosition.position;
                _projectileObjects[_curIndex].Target = target;
                float damage = _status.ATK;

                if (true == _isSplit)
                {
                    damage /= _maxShotCount * _countPerShot;
                }

                _projectileObjects[_curIndex].Damage = damage;

                _projectileObjectPool[_curIndex].SetActive(true);

                ++_curIndex;

                if (_objectPoolCount <= _curIndex)
                {
                    _curIndex = 0;
                }
            }

            ++_curShotCount;

            yield return new WaitForSeconds(_intervalTime);
        }

        _curShotCount = 0;
    }
}
