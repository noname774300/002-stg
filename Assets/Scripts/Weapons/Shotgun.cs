#nullable enable

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shotgun : IWeapon
{
    public string Name { get; private set; }
    public string TargetingMode => targetAttributeTexts[(int)nextTargetAttribute];
    private enum TargetAttribtue
    {
        Nearest,
        Weakest,
        Strongest
    }
    private TargetAttribtue nextTargetAttribute = TargetAttribtue.Nearest;
    private string[] targetAttributeTexts = { "近", "弱", "強" };
    public List<GameObject> Targets { get; private set; }
    private float lockOnRange;
    private float searchCooldown;
    private float searchTimer = 0;
    private float shootCooldown;
    private float shootTimer = 0;
    private float bulletVelocity;
    private int damage;

    public Shotgun(string name, float lockOnRange, float searchCooldown, float shootCooldown, float bulletVelocity, int damage)
    {
        Name = name;
        Targets = new List<GameObject>();
        this.lockOnRange = lockOnRange;
        this.searchCooldown = searchCooldown;
        this.shootCooldown = shootCooldown;
        this.bulletVelocity = bulletVelocity;
        this.damage = damage;
    }

    public void Update(GameObject user, string targetTag, bool loaded)
    {
        if (!loaded)
        {
            searchTimer = 0;
            Targets.Clear();
            shootTimer = 0;
            return;
        }
        var _ = Targets.RemoveAll(t =>
        {
            if (t == null)
            {
                return true;
            }
            var distance = (t.transform.position - user.transform.position).magnitude;
            return distance > lockOnRange;
        });
        if (searchTimer >= searchCooldown)
        {
            Targets.Clear();
            GameObject? target = null;
            var targetCandidates = GameObject.FindGameObjectsWithTag(targetTag);
            if (targetCandidates.Length >= 1)
            {
                switch (nextTargetAttribute)
                {
                    case TargetAttribtue.Nearest:
                        var minDistance = Mathf.Infinity;
                        foreach (var candidate in targetCandidates)
                        {
                            var distance = Vector3.Distance(user.transform.position, candidate.transform.position);
                            if (distance > lockOnRange)
                            {
                                continue;
                            }
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                target = candidate;
                            }
                        }
                        break;
                    case TargetAttribtue.Weakest:
                        var minHp = Mathf.Infinity;
                        foreach (var candidate in targetCandidates)
                        {
                            var distance = Vector3.Distance(user.transform.position, candidate.transform.position);
                            if (distance > lockOnRange)
                            {
                                continue;
                            }
                            candidate.GetComponent<ITarget>().IfNotNull(c =>
                            {
                                if (c.Hp < minHp)
                                {
                                    minHp = c.Hp;
                                    target = candidate;
                                }
                            });
                        }
                        break;
                    case TargetAttribtue.Strongest:
                        var maxHp = 0;
                        foreach (var candidate in targetCandidates)
                        {
                            var distance = Vector3.Distance(user.transform.position, candidate.transform.position);
                            if (distance > lockOnRange)
                            {
                                continue;
                            }
                            candidate.GetComponent<ITarget>().IfNotNull(c =>
                           {
                               if (c.Hp > maxHp)
                               {
                                   maxHp = c.Hp;
                                   target = candidate;
                               }
                           });
                        }
                        break;
                    default:
                        break;
                }
                searchTimer = 0;
            }
            if (target != null)
            {
                Targets.Add(target);
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
        Targets.Clear();
        searchTimer = 0;
    }

    public void PullTrigger(GameObject user, BattleScene battleScene)
    {
        if (shootTimer <= shootCooldown || Targets.Count == 0)
        {
            return;
        }
        var target = Targets[0];
        if (target == null)
        {
            return;
        }
        var targetDirection = (Targets[0].transform.position - user.transform.position).normalized;
        var bullet = Object.Instantiate(
            battleScene.BulletPrefab!,
            user.transform.position + (targetDirection * 1.2f),
            Quaternion.AngleAxis(
                (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90,
                Vector3.forward));
        bullet.Initialize(damage);
        bullet.GetComponent<Rigidbody2D>().IfNotNull(c => c.velocity = user.GetComponent<Rigidbody2D>().IfNotNull(c => c.velocity, Vector2.zero) + ((Vector2)targetDirection * bulletVelocity));
        shootTimer = 0;
    }
}
