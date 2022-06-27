#nullable enable

using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string Name { get; }
    string TargetingMode { get; }
    List<GameObject> Targets { get; }
    void Update(GameObject user, string targetTag, bool loaded);
    void ChangeTargetAttribute();
    void PullTrigger(GameObject user, BattleScene battleScene);
}
