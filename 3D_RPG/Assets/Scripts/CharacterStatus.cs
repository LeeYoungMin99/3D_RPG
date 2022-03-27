using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private float MaxHPPerLevel = 100f;
    [SerializeField] private float ATKPerLevel = 10f;

    private Animator _animator;
    private int _curExperience = 0;
    private int _requiredEXP = 2;
    private float _curHP = 1f;
    private DeathEventArgs _eventArgs = new DeathEventArgs();

    public event EventHandler<DeathEventArgs> OnDeathEvent;

    public int Level { get { return _level; } }
    public float ATK { get; private set; }
    public float MaxHP { get; private set; }
    public float CurHP { get { return _curHP; } }
    public int Experience { set { _eventArgs.Experience = value; } }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        --_level;

        LevelUp();
    }

    private void LevelUp()
    {
        ++_level;

        _curExperience = 0;
        _requiredEXP = 2;

        for (int i = 0; i < _level; ++i)
        {
            _requiredEXP *= 2;
        }

        ATK = ATKPerLevel * _level;
        MaxHP = MaxHPPerLevel * _level;

        _curHP = MaxHP;
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    public void Init()
    {
        _curHP = MaxHP;
    }

    public void TakeDamage(float Damage, EventHandler<DeathEventArgs> killEvent)
    {
        if (0f >= _curHP) return;

        _curHP -= Damage;

        if (0f >= _curHP)
        {
            _curHP = 0f;

            _animator.SetTrigger(CharacterAnimID.IS_DIE);

            OnDeathEvent -= killEvent;
            OnDeathEvent += killEvent;

            OnDeathEvent?.Invoke(this, _eventArgs);

            return;
        }

        _animator.SetTrigger(CharacterAnimID.IS_DAMAGE);
    }

    public void GainExperience(int exp)
    {
        _curExperience += exp;

        if (_curExperience < _requiredEXP) return;

        LevelUp();
    }
}
