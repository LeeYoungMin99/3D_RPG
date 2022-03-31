using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawn Setting")]
    [SerializeField] private GameObject _monster;
    [SerializeField] private int _monsterExperience;
    [SerializeField] private int _monsterID;
    [Range(1, 10)]
    [SerializeField] private int _maxCount = 1;
    [Range(20f, 30f)]
    [SerializeField] private float _respawnDelay = 20f;

    private void Awake()
    {
        for (int i = 0; i < _maxCount; ++i)
        {
            GameObject monster = Instantiate(_monster, transform);
            monster.tag = "Enemy";
            monster.layer = 3;
            monster.name = _monster.name;

            SetTransform(monster);

            monster.AddComponent<TargetManager>();
            EnemyDeath death = monster.AddComponent<EnemyDeath>();
            death.Experience = _monsterExperience;
            death.ID = _monsterID;

            monster.SetActive(true);

            death.EnemyDeathEvent -= RespawnAfterTimeHelper;
            death.EnemyDeathEvent += RespawnAfterTimeHelper;
        }
    }

    private void RespawnAfterTimeHelper(object sender, DeathEventArgs args)
    {
        StartCoroutine(RespawnAfterTime(args));
    }

    private IEnumerator RespawnAfterTime(DeathEventArgs args)
    {
        yield return new WaitForSeconds(_respawnDelay);

        GameObject monster = args.GameObject;

        monster.GetComponent<CharacterStatus>().Init();

        SetTransform(monster);

        monster.SetActive(true);
    }

    private void SetTransform(GameObject monster)
    {
        monster.transform.localPosition = SetRandomPosition();
        monster.transform.rotation = SetRandomRotation();
    }

    private Vector3 SetRandomPosition()
    {
        float x = UnityEngine.Random.Range(0f, 5f);
        float z = UnityEngine.Random.Range(0f, 5f);

        return new Vector3(x, 0f, z);
    }

    private Quaternion SetRandomRotation()
    {
        float y = UnityEngine.Random.Range(0f, 360f);

        return Quaternion.Euler(0f, y, 0f);
    }
}
