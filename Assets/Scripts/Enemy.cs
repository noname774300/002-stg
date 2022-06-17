using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movingForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var player = FindObjectOfType<Player>();
        if (!player) return;
        var direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * movingForce);
    }
}
