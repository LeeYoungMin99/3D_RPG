using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterInventorySlotManager _slotManager;

    private void Awake()
    {
        _slotManager.ObtainCharacter("Sword Man");

        new FirstFarmerTalk();
    }
}
