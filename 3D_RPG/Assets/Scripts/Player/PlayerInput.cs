using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _inputHorizontalName = "Horizontal";
    private string _inputVerticalName = "Vertical";
    private string _moveMouseHorizontalName = "Mouse X";
    private string _moveMouseVerticalName = "Mouse Y";
    private string _tagFirstCharacterKey = "1";
    private string _tagSecondCharacterKey = "2";
    private string _tagThirdCharacterKey = "3";
    private string _attackKeyName = "Attack";

    public float InputHorizontal { get; private set; }
    public float InputVertical { get; private set; }
    public float MoveMouseX { get; private set; }
    public float MoveMouseY { get; private set; }
    public bool InputTagFirstCharacter { get; private set; }
    public bool InputTagSecondCharacter { get; private set; }
    public bool InputTagThirdCharacter { get; private set; }
    public bool InputAttack { get; private set; }

    public void Init()
    {
        InputHorizontal = 0f;
        InputVertical = 0f;
        MoveMouseX = 0f;
        MoveMouseY = 0f;

        InputAttack = false;

        InputTagFirstCharacter = false;
        InputTagSecondCharacter = false;
        InputTagThirdCharacter = false;
    }
    void Update()
    {
        InputHorizontal = Input.GetAxis(_inputHorizontalName);
        InputVertical = Input.GetAxis(_inputVerticalName);
        MoveMouseX = Input.GetAxis(_moveMouseHorizontalName);
        MoveMouseY = Input.GetAxis(_moveMouseVerticalName);

        InputAttack = Input.GetButton(_attackKeyName);

        InputTagFirstCharacter = Input.GetKeyDown(_tagFirstCharacterKey);
        InputTagSecondCharacter = Input.GetKeyDown(_tagSecondCharacterKey);
        InputTagThirdCharacter = Input.GetKeyDown(_tagThirdCharacterKey);
    }
}
