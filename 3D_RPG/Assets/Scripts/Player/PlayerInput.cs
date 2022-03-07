using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _inputHorizontalName = "Horizontal";
    private string _inputVerticalName = "Vertical";
    private string _moveMouseHorizontalName = "Mouse X";
    private string _moveMouseVerticalName = "Mouse Y";
    private string _attackKeyName = "Attack";

    public float InputHorizontal { get; private set; }
    public float InputVertical { get; private set; }
    public float MoveMouseX { get; private set; }
    public float MoveMouseY { get; private set; }
    public bool InputAttack { get; private set; }

    void Update()
    {
        InputHorizontal = Input.GetAxis(_inputHorizontalName);
        InputVertical = Input.GetAxis(_inputVerticalName);
        MoveMouseX = Input.GetAxis(_moveMouseHorizontalName);
        MoveMouseY = Input.GetAxis(_moveMouseVerticalName);

        InputAttack = Input.GetButton(_attackKeyName);
    }
}
