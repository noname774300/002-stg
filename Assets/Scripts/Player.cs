using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movingForce;
    [SerializeField] private float rotatingForce;
    private BattleScene battleScene;
    private Input input;
    private Rigidbody2D rb;

    void Start()
    {
        battleScene = FindObjectOfType<BattleScene>();
        input = FindObjectOfType<Input>();
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

    void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(
            Mathf.Clamp(
                position.x,
                -battleScene.PlayerMovingLimit.x,
                battleScene.PlayerMovingLimit.x),
            Mathf.Clamp(
                position.y,
                -battleScene.PlayerMovingLimit.y,
                battleScene.PlayerMovingLimit.y),
            position.z);
    }
}
