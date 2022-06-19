using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour
{
    public InputActions InputActions;
    public Vector2 MoveDirection = Vector2.zero;
    public Vector2 LookDirection = Vector2.zero;
    private DynamicJoystick moveJoystick;
    private DynamicJoystick lookJoystick;

    void Start()
    {
        InputActions = new InputActions();
        InputActions.Enable();
        moveJoystick = GameObject.Find("MoveJoystick").GetComponent<DynamicJoystick>();
        lookJoystick = GameObject.Find("LookJoystick").GetComponent<DynamicJoystick>();
    }

    void Update()
    {
        var inputActionsMoveDirection = InputActions.Player.Move.ReadValue<Vector2>();
        MoveDirection = inputActionsMoveDirection.magnitude > 0 ? inputActionsMoveDirection : moveJoystick.Direction;
        var inputActionsLookDirection = InputActions.Player.Look.ReadValue<Vector2>();
        LookDirection = inputActionsLookDirection.magnitude > 0 ? inputActionsLookDirection : lookJoystick.Direction;
    }
}
