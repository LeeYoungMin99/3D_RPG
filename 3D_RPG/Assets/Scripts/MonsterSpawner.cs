using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private GameObject _monster;
    [Range(1, 10)]
    [SerializeField] private int _maxCount = 1;
    [Range(20f, 30f)]
    [SerializeField] private float _respawnDelay = 20f;

    private GameObject[] _monsters;

    private void Awake()
    {
        _monsters = new GameObject[_maxCount];

        for (int i = 0; i < _maxCount; ++i)
        {
            _monsters[i] = Instantiate(_monster, transform.position + SetRandomPosition(), Quaternion.Euler(SetRandomRotation()), transform);
            _monsters[i].name = _monster.name;
            MonsterSpawn spawn = _monsters[i].AddComponent<MonsterSpawn>();

            spawn.MonsterDeathEvent -= RespawnAfterTimeHelper;
            spawn.MonsterDeathEvent += RespawnAfterTimeHelper;
        }
    }

    private void RespawnAfterTimeHelper(object sender, DeathEventArgs args)
    {
        StartCoroutine(RespawnAfterTime(args));
    }

    private IEnumerator RespawnAfterTime(DeathEventArgs args)
    {
        yield return new WaitForSeconds(_respawnDelay);

        args.GameObject.GetComponent<CharacterStatus>().Init();
        args.GameObject.transform.position = transform.position + SetRandomPosition();
        args.GameObject.transform.rotation = Quaternion.Euler(SetRandomRotation());
        args.GameObject.SetActive(true);
    }

    private Vector3 SetRandomPosition()
    {
        float x = UnityEngine.Random.Range(0f, 5f);
        float y = UnityEngine.Random.Range(0f, 5f);
        float z = UnityEngine.Random.Range(0f, 5f);

        return new Vector3(x, y, z);
    }

    private Vector3 SetRandomRotation()
    {
        float y = UnityEngine.Random.Range(0f, 360f);

        return new Vector3(0f, y, 0f);
    }
}
