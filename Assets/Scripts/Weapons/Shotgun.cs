#nullable enable
namespace Weapons
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Shotgun : IWeapon
    {
        public string Name => name;
        private string name;
        public string TargetingMode
        {
            get
            {
                return targetAttributeTexts[(int)nextTargetAttribute];
            }
        }
        private enum TargetAttribtue
        {
            Nearest,
            Weakest,
            Strongest
        }
        private TargetAttribtue nextTargetAttribute = TargetAttribtue.Nearest;
        private string[] targetAttributeTexts = { "近", "弱", "強" };
        public List<Enemy> Targets => targets;
        private List<Enemy> targets;
        private float lockOnRange;
        private float searchCooldown;
        private float searchTimer = 0;
        private float shootCooldown;
        private float shootTimer = 0;
        private float bulletVelocity;
        private int damage;
        private int pellets;

        public Shotgun(string name, float lockOnRange, float searchCooldown, float shootCooldown, float bulletVelocity, int damage, int pellets)
        {
            this.name = name;
            targets = new List<Enemy>();
            this.lockOnRange = lockOnRange;
            this.searchCooldown = searchCooldown;
            this.shootCooldown = shootCooldown;
            this.bulletVelocity = bulletVelocity;
            this.damage = damage;
            this.pellets = pellets;
        }

        public void Update(Player player, bool loaded)
        {
            if (!loaded)
            {
                searchTimer = 0;
                targets.Clear();
                shootTimer = 0;
                return;
            }
            targets.RemoveAll(t =>
            {
                if (t == null) return true;
                var distance = (t.transform.position - player.transform.position).magnitude;
                return distance > lockOnRange;
            });
            if (searchTimer >= searchCooldown)
            {
                targets.Clear();
                Enemy? target = null;
                var enemies = Object.FindObjectsOfType<Enemy>();
                if (enemies.Length >= 1)
                {
                    switch (nextTargetAttribute)
                    {
                        case TargetAttribtue.Nearest:
                            var minDistance = Mathf.Infinity;
                            foreach (var enemy in enemies)
                            {
                                var distance = Vector3.Distance(player.transform.position, enemy.transform.position);
                                if (distance > lockOnRange) continue;
                                if (distance < minDistance)
                                {
                                    minDistance = distance;
                                    target = enemy;
                                }
                            }
                            break;
                        case TargetAttribtue.Weakest:
                            var minHp = Mathf.Infinity;
                            foreach (var enemy in enemies)
                            {
                                var distance = Vector3.Distance(player.transform.position, enemy.transform.position);
                                if (distance > lockOnRange) continue;
                                if (enemy.Hp < minHp)
                                {
                                    minHp = enemy.Hp;
                                    target = enemy;
                                }
                            }
                            break;
                        case TargetAttribtue.Strongest:
                            var maxHp = 0;
                            foreach (var enemy in enemies)
                            {
                                var distance = Vector3.Distance(player.transform.position, enemy.transform.position);
                                if (distance > lockOnRange) continue;
                                if (enemy.Hp > maxHp)
                                {
                                    maxHp = enemy.Hp;
                                    target = enemy;
                                }
                            }
                            break;
                    }
                    searchTimer = 0;
                }
                if (target != null)
                {
                    targets.Add(target);
                }
            }
            else
            {
                searchTimer += Time.deltaTime;
            }
            if (shootTimer <= shootCooldown)
            {
                shootTimer += Time.deltaTime;
            }
        }

        public void ChangeTargetAttribute()
        {
            nextTargetAttribute = (TargetAttribtue)(((int)nextTargetAttribute + 1) % 3);
            targets.Clear();
            searchTimer = 0;
        }

        public void PullTrigger(Player player, BattleScene battleScene)
        {
            if (shootTimer <= shootCooldown) return;
            if (targets.Count == 0) return;
            var targetDirection = (targets[0].transform.position - player.transform.position).normalized;
            var bullet = Object.Instantiate(
                battleScene.BulletPrefab!,
                player.transform.position + targetDirection * 1.2f,
                Quaternion.AngleAxis(
                    Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90,
                    Vector3.forward));
            bullet.Initialize(damage);
            bullet.GetComponent<Rigidbody2D>().velocity = player.Rb.velocity + (Vector2)targetDirection * bulletVelocity;
            shootTimer = 0;
        }
    }
}
