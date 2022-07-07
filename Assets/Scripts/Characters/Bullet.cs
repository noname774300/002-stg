#nullable enable
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    public void Initialize(int damage) => this.damage = damage;

    protected void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        collisionInfo.gameObject.GetComponent<IDamageTaker>().IfNotNull(c => c.TakeDamage(damage));
        Destroy(gameObject);
    }
}
