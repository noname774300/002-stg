#nullable enable
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageTaker, ITarget
{
    public int Hp { get; private set; }
    private float movingForce;
    private Rigidbody2D? rb;
    private Player? player;
    [SerializeReference] private IWeapon? weapon;
    private BattleScene? battleScene;

    public void Initialize(int maxHp, float movingForce)
    {
        Hp = maxHp;
        this.movingForce = movingForce;
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        weapon = new Shotgun(
            name: "ショットガン",
            lockOnRange: 5,
            searchCooldown: 0.5f,
            shootCooldown: 1,
            bulletVelocity: 40,
            damage: 10);
        battleScene = FindObjectOfType<BattleScene>();
    }

    protected void Update()
    {
        weapon!.Update(gameObject, "Player", true);
        weapon.PullTrigger(gameObject, battleScene!);
    }

    protected void FixedUpdate()
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
