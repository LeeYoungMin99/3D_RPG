using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";
    private const string ATTACK = "Attack";
    private const string SKILL = "Skill";

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public float MoveMouseX { get; private set; }
    public float MoveMouseY { get; private set; }
    public bool Attack { get; private set; }
    public bool Skill { get; private set; }

    private void Update()
    { 
        Horizontal = Input.GetAxis(HORIZONTAL);
        Vertical = Input.GetAxis(VERTICAL);
        MoveMouseX = Input.GetAxis(MOUSE_X);
        MoveMouseY = Input.GetAxis(MOUSE_Y);

        if (true == EventSystem.current.IsPointerOverGameObject()) return;

        Attack = Input.GetButtonDown(ATTACK);
        Skill = Input.GetButtonDown(SKILL);
    }
}
