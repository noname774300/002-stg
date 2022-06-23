#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp => hp;
    private int hp;
    private int maxHp;
    private float movingForce;
    private Rigidbody2D? rb;
    private Player? player;

    public void Initialize(int maxHp, float movingForce)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        this.movingForce = movingForce;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        var direction = (player!.transform.position - transform.position).normalized;
        rb!.AddForce(direction * movingForce);
    }

    public void TakeDamage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
