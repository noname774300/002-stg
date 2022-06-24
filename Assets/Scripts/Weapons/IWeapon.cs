#nullable enable
namespace Weapons
{
    using System.Collections.Generic;

    public interface IWeapon
    {
        string Name { get; }
        string TargetingMode { get; }
        List<Enemy> Targets { get; }
        void Update(Player player, bool loaded);
        void ChangeTargetAttribute();
        void PullTrigger(Player player, BattleScene battleScene);
    }
}
