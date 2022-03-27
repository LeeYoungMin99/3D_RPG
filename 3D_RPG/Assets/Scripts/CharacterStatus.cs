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
    private DeathEventArgs _deathEventArgs = new DeathEventArgs();
    private DataChangeEventArgs _dataChangeEventArgs = new DataChangeEventArgs();

    public event EventHandler<DeathEventArgs> OnDeathEvent;
    public event EventHandler<DataChangeEventArgs> OnChangeDataEvent;

    public int Level { get { return _level; } }
    public float ATK { get; private set; }
    public float MaxHP { get; private set; }
    public float CurHP { get { return _curHP / MaxHP; } }
    public float CurExperience { get { return (float)_curExperience / (float)_requiredEXP; } }
    public int Experience { set { _deathEventArgs.Experience = value; } }

    private void OnEnable()
    {
        CallChangeDataEvent();
    }

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

        CallChangeDataEvent();
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    public void CallChangeDataEvent()
    {
        _dataChangeEventArgs.Name = gameObject.name;
        _dataChangeEventArgs.Level = _level;
        _dataChangeEventArgs.ATK = ATK;
        _dataChangeEventArgs.NormalizedCurHP = CurHP;
        _dataChangeEventArgs.MaxHP = MaxHP;
        _dataChangeEventArgs.CurHP = _curHP;
        _dataChangeEventArgs.CurExperience = CurExperience;

        OnChangeDataEvent?.Invoke(this, _dataChangeEventArgs);
    }

    public void Init()
    {
        _curHP = MaxHP;

        OnChangeDataEvent?.Invoke(this, _dataChangeEventArgs);
    }

    public void TakeDamage(float Damage, EventHandler<DeathEventArgs> killEvent)
    {
        if (0f >= _curHP) return;

        _curHP -= Damage;

        CallChangeDataEvent();

        if (0f >= _curHP)
        {
            _curHP = 0f;

            _animator.SetTrigger(CharacterAnimID.IS_DIE);

            OnDeathEvent -= killEvent;
            OnDeathEvent += killEvent;

            OnDeathEvent?.Invoke(this, _deathEventArgs);

            return;
        }

        _animator.SetTrigger(CharacterAnimID.IS_DAMAGE);
    }

    public void GainExperience(int exp)
    {
        _curExperience += exp;

        CallChangeDataEvent();

        if (_curExperience < _requiredEXP) return;

        LevelUp();
    }
}
