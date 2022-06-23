#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    public void Initialize(int damage)
    {
        this.damage = damage;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Enemy")
        {
            collisionInfo.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
