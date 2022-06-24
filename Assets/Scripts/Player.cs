using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public WeaponsHolder WeaponsHolder { get; private set; }
    private BattleScene battleScene;
    private Vector2 movingLimit;
    private float movingForce;
    private float rotatingForce;

    public void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        MaxHp = 100;
        Hp = MaxHp;
        WeaponsHolder = new WeaponsHolder(new[] {
            new Shotgun(
                name: "ショットガン",
                lockOnRange: 5,
                searchCooldown: 0.5f,
                shootCooldown: 1,
                bulletVelocity: 40,
                damage: 1),
            new Shotgun(
                name: "ライフル",
                lockOnRange: 10,
                searchCooldown: 1,
                shootCooldown: 0.2f,
                bulletVelocity: 20,
                damage: 2) });
        battleScene = FindObjectOfType<BattleScene>();
        movingLimit = new Vector2(15, 15);
        movingForce = 40;
        rotatingForce = 1;
    }

    public void Update()
    {
        WeaponsHolder.Update(this);
        if (battleScene.Input.InputActions.Player.ChangeWeapon.triggered)
        {
            WeaponsHolder.ChangeWeapon();
        }
        if (battleScene.Input.InputActions.Player.ChangeTarget.triggered)
        {
            WeaponsHolder.ChangeTargetAttributeOfLoadedWeapon();
        }
        if (battleScene.Input.InputActions.Player.Fire.IsPressed())
        {
            WeaponsHolder.PullTriggerOfLoadedWeapon(this, battleScene);
        }
    }

    public void FixedUpdate()
    {
        var lookDirection = battleScene.Input.LookDirection;
        if (lookDirection.magnitude > 0)
        {
            Rb.AddTorque(-lookDirection.x * rotatingForce);
        }
        var moveDirection = battleScene.Input.MoveDirection;
        if (moveDirection.magnitude > 0)
        {
            Rb.AddRelativeForce(moveDirection * movingForce);
        }
    }

    public void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(
            Mathf.Clamp(position.x, -movingLimit.x, movingLimit.x),
            Mathf.Clamp(position.y, -movingLimit.y, movingLimit.y),
            position.z);
    }
}
