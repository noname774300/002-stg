#nullable enable
using UnityEngine;

[System.Serializable]
public class Input
{
    public Vector2 MoveDirection { get; private set; }
    private DynamicJoystick moveJoystick;
    public InputActions InputActions
    {
        get
        {
            if (inputActions == null)
            {
                inputActions = new InputActions();
                inputActions.Enable();
            }
            return inputActions;
        }
    }
    private InputActions? inputActions;

    public Input(DynamicJoystick moveJoystick) => this.moveJoystick = moveJoystick;

    public void Update()
    {
        var inputActionsMoveDirection = InputActions.Player.Move.ReadValue<Vector2>();
        MoveDirection = inputActionsMoveDirection.magnitude > 0 ? inputActionsMoveDirection : moveJoystick.Direction;
    }
}
