using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private float MaxHPPerLevel = 100f;
    [SerializeField] private float ATKPerLevel = 100f;
    [SerializeField] private int _level = 1;

    private int _curEXP;
    private int _requiredEXP;
    private float _curHP;

    public float ATK { get; private set; }
    public float MaxHP { get; private set; }
    public float CurHP { get; private set; }

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

        CurHP = MaxHP;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float Damage)
    {
        if (0f >= CurHP) return;

        CurHP -= Damage;

        if (0f >= CurHP)
        {
            CurHP = 0f;

            Die();
        }
    }
}
