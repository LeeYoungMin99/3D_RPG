using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeath : State
{
    [SerializeField] private int _experience = 1;

    protected override void Awake()
    {
        base.Awake();

        GetComponent<CharacterStatus>().Experience = _experience;
    }
}
