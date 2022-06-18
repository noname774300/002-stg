using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movingForce;
    private InputActions inputActions;
    private DynamicJoystick moveJoystick;
    private DynamicJoystick lookJoystick;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lookDirection;

    void Start()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Fire.performed += context => Debug.Log("Fire");
        moveJoystick = GameObject.Find("MoveJoystick").GetComponent<DynamicJoystick>();
        lookJoystick = GameObject.Find("LookJoystick").GetComponent<DynamicJoystick>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var inputActionsMoveDirection = inputActions.Player.Move.ReadValue<Vector2>();
        moveDirection = inputActionsMoveDirection.magnitude > 0 ? inputActionsMoveDirection : moveJoystick.Direction;
        var inputActionsLookDirection = inputActions.Player.Look.ReadValue<Vector2>();
        lookDirection = inputActionsLookDirection.magnitude > 0 ? inputActionsLookDirection : lookJoystick.Direction;
    }

    void FixedUpdate()
    {
        if (moveDirection.magnitude > 0)
        {
            rb.AddForce(moveDirection * movingForce);
        }
    }
}
