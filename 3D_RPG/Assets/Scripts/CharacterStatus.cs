using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private float MaxHPPerLevel = 100f;
    [SerializeField] private float ATKPerLevel = 10f;

    private int _curEXP;
    private int _requiredEXP;
    private float _curHP;

    public int Level { get { return _level; } }
    public float ATK { get; private set; }
    public float MaxHP { get; private set; }
    public float CurHP { get { return _curHP; } }

    private void Start()
    {
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

    private void Die()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float Damage)
    {
        if (0f >= CurHP) return;

        _curHP -= Damage;

        if (0f >= CurHP)
        {
            _curHP = 0f;

            Die();
        }
    }
}
