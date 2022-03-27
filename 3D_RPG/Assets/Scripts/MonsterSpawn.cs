using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    private DeathEventArgs _spawnEvent = new DeathEventArgs();

    public event EventHandler<DeathEventArgs> MonsterDeathEvent;

    private void Awake()
    {
        _spawnEvent.GameObject = gameObject;

        GetComponent<CharacterStatus>().OnDeathEvent -= CallSpawn;
        GetComponent<CharacterStatus>().OnDeathEvent += CallSpawn;
    }

    private void CallSpawn(object sender, EventArgs args)
    {
        MonsterDeathEvent?.Invoke(this, _spawnEvent);
    }
}
