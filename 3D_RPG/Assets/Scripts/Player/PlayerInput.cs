using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTOCAL = "Vertical";
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";
    private const string ATTACK = "Attack";
    private const string SKILL = "Skill";
    private const string CAMERA_LOCK = "Camera Lock";

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public float MoveMouseX { get; private set; }
    public float MoveMouseY { get; private set; }
    public bool Attack { get; private set; }
    public bool Skill { get; private set; }
    public bool CameraLock { get; private set; }


    private void Update()
    {
        Horizontal = Input.GetAxis(HORIZONTAL);
        Vertical = Input.GetAxis(VERTOCAL);
        MoveMouseX = Input.GetAxis(MOUSE_X);
        MoveMouseY = Input.GetAxis(MOUSE_Y);
        CameraLock = Input.GetButtonDown(CAMERA_LOCK);

        if (true == PointUI()) return;

        Attack = Input.GetButtonDown(ATTACK);
        Skill = Input.GetButtonDown(SKILL);
    }

    private bool PointUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
