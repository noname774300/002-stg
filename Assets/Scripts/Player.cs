using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rb => rb;
    private Rigidbody2D rb;
    public int MaxHp => maxHp;
    private int maxHp;
    public int Hp => hp;
    private int hp;
    public WeaponsHolder WeaponsHolder => weaponsHolder;
    private WeaponsHolder weaponsHolder;
    private BattleScene battleScene;
    private Vector2 movingLimit;
    private float movingForce;
    private float rotatingForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHp = 100;
        hp = maxHp;
        weaponsHolder = new WeaponsHolder(new[] {
            new Shotgun(
                name: "ショットガン",
                lockOnRange: 5,
                searchCooldown: 0.5f,
                shootCooldown: 1,
                bulletVelocity: 40,
                damage: 1,
                pellets: 10),
            new Shotgun(
                name: "ライフル",
                lockOnRange: 10,
                searchCooldown: 1,
                shootCooldown: 0.2f,
                bulletVelocity: 20,
                damage: 2,
                pellets: 1) });
        battleScene = FindObjectOfType<BattleScene>();
        movingLimit = new Vector2(15, 15);
        movingForce = 40;
        rotatingForce = 1;
    }

    void Update()
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

    void FixedUpdate()
    {
        var lookDirection = battleScene.Input.LookDirection;
        if (lookDirection.magnitude > 0)
        {
            rb.AddTorque(-lookDirection.x * rotatingForce);
        }
        var moveDirection = battleScene.Input.MoveDirection;
        if (moveDirection.magnitude > 0)
        {
            rb.AddRelativeForce(moveDirection * movingForce);
        }
    }

    void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(
            Mathf.Clamp(position.x, -movingLimit.x, movingLimit.x),
            Mathf.Clamp(position.y, -movingLimit.y, movingLimit.y),
            position.z);
    }
}
