#nullable enable
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp { get; private set; }
    private float movingForce;
    private Rigidbody2D? rb;
    private Player? player;

    public void Initialize(int maxHp, float movingForce)
    {
        Hp = maxHp;
        this.movingForce = movingForce;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    public void FixedUpdate()
    {
        var direction = (player!.transform.position - transform.position).normalized;
        rb!.AddForce(direction * movingForce);
    }

    public void TakeDamage(int damage)
    {
        Hp = Mathf.Max(0, Hp - damage);
        if (Hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
