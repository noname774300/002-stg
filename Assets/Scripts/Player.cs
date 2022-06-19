using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movingForce;
    [SerializeField] private float rotatingForce;
    private Input input;
    private Rigidbody2D rb;

    void Start()
    {
        input = GameObject.FindObjectOfType<Input>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var lookDirection = input.LookDirection;
        if (lookDirection.magnitude > 0)
        {
            rb.AddTorque(-lookDirection.x * rotatingForce);
        }
        var moveDirection = input.MoveDirection;
        if (moveDirection.magnitude > 0)
        {
            rb.AddRelativeForce(moveDirection * movingForce);
        }
    }
}
