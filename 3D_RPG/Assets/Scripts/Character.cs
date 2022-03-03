using UnityEngine;

public class Character
{
    public Character(string name, int hp, int atk, int def)
    {
        Name = name;
        Level = 0;
        _baseHP = hp;
        _baseATK = atk;
        _baseDEF = def;

        LevelUp();
    }

    public string Name { get; private set; }
    public int Level { get; private set; }
    public int MaxHP { get; private set; }
    public int ATK { get; private set; }
    public int DEF { get; private set; }

    public bool bIsDeployment = false;

    public GameObject CharacterPwan;

    private int _baseHP;
    private int _baseATK;
    private int _baseDEF;

    void LevelUp()
    {
        ++Level;

        MaxHP = Level * _baseHP;
        ATK = Level * _baseATK;
        DEF = Level * _baseDEF;
    }

    public void EnableCharacter()
    {
        CharacterPwan.SetActive(true);
    }

    public void DisableCharacter()
    {
        CharacterPwan.SetActive(false);
    }
}
