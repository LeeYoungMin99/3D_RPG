using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _inputHorizontalName = "Horizontal";
    private string _inputVerticalName = "Vertical";
    private string _moveMouseHorizontalName = "Mouse X";
    private string _moveMouseVerticalName = "Mouse Y";
    private string _TagFirstCharacterKeyName = "1";
    private string _TagSecondCharacterKeyKeyName = "2";
    private string _TagThirdCharacterKeyKeyName = "3";

    public float InputHorizontal { get; private set; }
    public float InputVertical { get; private set; }
    public float MoveMouseX { get; private set; }
    public float MoveMouseY { get; private set; }
    public bool InputTagFirstCharacter { get; private set; }
    public bool InputTagSecondCharacter { get; private set; }
    public bool InputTagThirdCharacter { get; private set; }

    public void Init()
    {
        InputHorizontal = 0f;
        InputVertical = 0f;
        MoveMouseX = 0f;
        MoveMouseY = 0f;

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

        InputTagFirstCharacter = Input.GetKeyDown(_TagFirstCharacterKeyName);
        InputTagSecondCharacter = Input.GetKeyDown(_TagSecondCharacterKeyKeyName);
        InputTagThirdCharacter = Input.GetKeyDown(_TagThirdCharacterKeyKeyName);
    }
}
