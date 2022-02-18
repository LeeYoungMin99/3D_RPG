using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _horizontalInputName = "Horizontal";
    private string _verticalInputName = "Vertical";
    private string _xMouseMoveName = "Mouse X";
    private string _yMouseMoveName = "Mouse Y";

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public float MouseXMove { get; private set; }
    public float MouseYMove { get; private set; }

    void Update()
    {
        HorizontalInput = Input.GetAxis(_horizontalInputName);
        VerticalInput = Input.GetAxis(_verticalInputName);
        MouseXMove = Input.GetAxis(_xMouseMoveName);
        MouseYMove = Input.GetAxis(_yMouseMoveName);
    }
}
