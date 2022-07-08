#nullable enable

using UnityEngine;

public class Player : MonoBehaviour, IDamageTaker, ITarget
{
    public Rigidbody2D? Rb { get; private set; }
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public WeaponsHolder? WeaponsHolder => weaponsHolder;
    [SerializeReference] private WeaponsHolder? weaponsHolder;
    private BattleScene? battleScene;
    private Vector2 movingLimit;
    private float movingForce;

    protected void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        MaxHp = 100;
        Hp = MaxHp;
        weaponsHolder = new WeaponsHolder(new[] {
            new Shotgun(
                name: "ショットガン",
                lockOnRange: 5,
                searchCooldown: 0.5f,
                shootCooldown: 1,
                bulletVelocity: 40,
                damage: 10),
            new Shotgun(
                name: "ライフル",
                lockOnRange: 10,
                searchCooldown: 1,
                shootCooldown: 0.2f,
                bulletVelocity: 20,
                damage: 5) });
        battleScene = FindObjectOfType<BattleScene>();
        movingLimit = new Vector2(15, 15);
        movingForce = 40;
    }

    protected void Update()
    {
        WeaponsHolder!.Update(gameObject, "Enemy");
        if (battleScene!.Input!.InputActions.Player.ChangeWeapon.triggered)
        {
            WeaponsHolder.ChangeWeapon();
        }
        if (battleScene.Input.InputActions.Player.ChangeTarget.triggered)
        {
            WeaponsHolder.ChangeTargetAttributeOfLoadedWeapon();
        }
        if (battleScene.Input.InputActions.Player.Fire.IsPressed())
        {
            WeaponsHolder.PullTriggerOfLoadedWeapon(gameObject, battleScene);
        }
    }

    protected void FixedUpdate()
    {
        var moveDirection = battleScene!.Input!.MoveDirection;
        if (moveDirection.magnitude > 0)
        {
            Rb!.AddRelativeForce(moveDirection * movingForce);
        }
    }

    protected void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(
            Mathf.Clamp(position.x, -movingLimit.x, movingLimit.x),
            Mathf.Clamp(position.y, -movingLimit.y, movingLimit.y),
            position.z);
    }

    public void TakeDamage(int damage) => Hp = Mathf.Max(0, Hp - damage);
}
