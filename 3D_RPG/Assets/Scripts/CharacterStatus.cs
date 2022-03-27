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
    private int _curEXP;
    private int _requiredEXP;
    private float _curHP = 1f;

    public event EventHandler<EventArgs> OnDeath;

    public int Level { get { return _level; } }
    public float ATK { get; private set; }
    public float MaxHP { get; private set; }
    public float CurHP { get { return _curHP; } }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        --_level;

        LevelUp();
    }

    private void LevelUp()
    {
        ++_level;

        _curEXP -= _requiredEXP;
        _requiredEXP *= 2;

        ATK = ATKPerLevel * _level;
        MaxHP = MaxHPPerLevel * _level;

        _curHP = MaxHP;
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float Damage)
    {
        if (0f >= _curHP) return;

        _curHP -= Damage;

        if (0f >= _curHP)
        {
            _curHP = 0f;

            _animator.SetTrigger(CharacterAnimID.IS_DIE);

            OnDeath?.Invoke(this,EventArgs.Empty);

            return;
        }

        _animator.SetTrigger(CharacterAnimID.IS_DAMAGE);
    }
}
