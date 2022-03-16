using System.Collections;
using UnityEngine;

public class ThrowAttackState : AttackState
{
    [SerializeField] private ThrownObject _thrownObjectPrefab;
    [SerializeField] private float _thrownObjectSpeed = 20f;
    [SerializeField] private Transform _startPosition;

    private const int OBJECT_COUNT = 4;
    private GameObject[] _thrownObjectPool = new GameObject[OBJECT_COUNT];
    private ThrownObject[] _thrownObjects = new ThrownObject[OBJECT_COUNT];
    private int _curIndex = 0;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < OBJECT_COUNT; ++i)
        {
            _thrownObjectPool[i] = Instantiate(_thrownObjectPrefab.gameObject, _startPosition.position, transform.rotation);
            _thrownObjects[i] = _thrownObjectPool[i].GetComponent<ThrownObject>();
            _thrownObjects[i].Speed = _thrownObjectSpeed;
            _thrownObjectPool[i].SetActive(false);
        }
    }

    public override void EnterState()
    {
        if (null == _targetManager.EnemyTarget) return;

        StartCoroutine(Attack());

        ++_curIndex;

        if (_curIndex >= OBJECT_COUNT)
        {
            _curIndex = 0;
        }
    }

    public override IEnumerator Attack()
    {
        int index = _curIndex;

        _thrownObjectPool[index].transform.position = _startPosition.position;
        _thrownObjects[index].Target = _targetManager.EnemyTarget;
        _thrownObjects[index].Damage = _status.ATK;

        if (_delay > 0f)
        {
            yield return new WaitForSeconds(_delay);
        }

        _thrownObjectPool[index].SetActive(true);
    }
}
