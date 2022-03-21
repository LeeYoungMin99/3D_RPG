using System.Collections;
using UnityEngine;

public class ThrowAttackState : AttackState
{
    [SerializeField] private ThrownObject _thrownObjectPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private int _maxShotCount = 1;
    [SerializeField] private int _countPerShot = 1;
    [Range(0f, 0.5f)]
    [SerializeField] private float _intervalTime = 0.5f;

    private GameObject[] _thrownObjectPool;
    private ThrownObject[] _thrownObjects;
    private int _objectPoolCount;
    private int _curIndex = 0;
    private int _curShotCount = 0;

    protected override void Start()
    {
        base.Start();

        _objectPoolCount = _maxShotCount * _countPerShot * 2;

        _thrownObjectPool = new GameObject[_objectPoolCount];
        _thrownObjects = new ThrownObject[_objectPoolCount];

        GameObject thrownObjects = new GameObject("Thrown Objects");
        thrownObjects.transform.SetParent(GameObject.Find("Field").transform);

        for (int i = 0; i < _objectPoolCount; ++i)
        {
            _thrownObjectPool[i] = Instantiate(_thrownObjectPrefab.gameObject, _startPosition.position, transform.rotation);
            _thrownObjectPool[i].transform.SetParent(thrownObjects.transform);
            _thrownObjects[i] = _thrownObjectPool[i].GetComponent<ThrownObject>();
            _thrownObjects[i].Owner = _startPosition;
            _thrownObjects[i].TargetLayer = _targetManager.EnemyTargetLayer;
        }
    }

    protected override IEnumerator Attack()
    {
        if (_attackDelayTime > 0f) yield return new WaitForSeconds(_attackDelayTime);

        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        while (_maxShotCount > _curShotCount)
        {
            for (int i = 0; i < _countPerShot; ++i)
            {
                _thrownObjectPool[_curIndex].transform.position = _startPosition.position;
                _thrownObjects[_curIndex].Target = _targetManager.EnemyTarget;
                _thrownObjects[_curIndex].Damage = _status.ATK / (_maxShotCount * _countPerShot);

                _thrownObjectPool[_curIndex].SetActive(true);

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
