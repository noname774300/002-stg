#nullable enable
using UnityEngine;

[System.Serializable]
public class Input
{
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LookDirection { get; private set; }
    private DynamicJoystick moveJoystick;
    private DynamicJoystick lookJoystick;
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

    public Input(DynamicJoystick moveJoystick, DynamicJoystick lookJoystick)
    {
        this.moveJoystick = moveJoystick;
        this.lookJoystick = lookJoystick;
    }

    public void Update()
    {
        var inputActionsMoveDirection = InputActions.Player.Move.ReadValue<Vector2>();
        MoveDirection = inputActionsMoveDirection.magnitude > 0 ? inputActionsMoveDirection : moveJoystick.Direction;
        var inputActionsLookDirection = InputActions.Player.Look.ReadValue<Vector2>();
        LookDirection = inputActionsLookDirection.magnitude > 0 ? inputActionsLookDirection : lookJoystick.Direction;
    }
}
