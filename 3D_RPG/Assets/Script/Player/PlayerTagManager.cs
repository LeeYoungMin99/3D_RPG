using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTagManager : MonoBehaviour
{
    private List<GameObject> _playerCharacters = new List<GameObject>();
    [SerializeField] private PlayerInput _input;

    public int CurrentCharacterIndex { get; private set; } = 0;
    private const int _firstCharacterIndex = 0;
    private const int _secondCharacterIndex = 1;
    private const int _thirdCharacterIndex = 2;

    private void Start()
    {
        GameObject[] objs = FindObjectsOfType<GameObject>();

        foreach (var obj in objs)
        {
            if (obj.tag == "Player")
            {
                _playerCharacters.Add(obj);
                obj.SetActive(false);
            }
        }

        _playerCharacters[_thirdCharacterIndex].SetActive(true);
        CurrentCharacterIndex = _thirdCharacterIndex;
    }

    void Update()
    {
        if (_input.InputTagFirstCharacter && CurrentCharacterIndex != _firstCharacterIndex)
        {
            TagCharacter(_firstCharacterIndex);
        }
        else if (_input.InputTagSecondCharacter && CurrentCharacterIndex != _secondCharacterIndex)
        {
            TagCharacter(_secondCharacterIndex);
        }
        else if (_input.InputTagThirdCharacter && CurrentCharacterIndex != _thirdCharacterIndex)
        {
            TagCharacter(_thirdCharacterIndex);
        }
    }

    void TagCharacter(int index)
    {
        _input.Init();

        _playerCharacters[index].transform.rotation = _playerCharacters[CurrentCharacterIndex].transform.rotation;

        _playerCharacters[index].SetActive(true);
        _playerCharacters[CurrentCharacterIndex].SetActive(false);

        CurrentCharacterIndex = index;
    }
}
