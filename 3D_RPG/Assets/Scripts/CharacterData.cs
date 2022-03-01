public class CharacterData
{
    public CharacterData(string name, int hp, int atk, int def)
    {
        Name = name;
        Level = 0;
        _baseHP = hp;
        _baseATK = atk;
        _baseDEF = def;

        LevelUp();
    }

    public string Name;
    public int Level;
    public int MaxHP;
    public int ATK;
    public int DEF;

    public bool IsDeployment = false;

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
}
