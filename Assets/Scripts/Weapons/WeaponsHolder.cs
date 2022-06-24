#nullable enable
namespace Weapons
{
    using UnityEngine;

    [System.Serializable]
    public class WeaponsHolder
    {
        public IWeapon[] Weapons => weapons;
        [SerializeReference] private IWeapon[] weapons;
        private int loadedWeaponIndex = 0;

        public WeaponsHolder(IWeapon[] weapons) => this.weapons = weapons;

        public IWeapon? GetLoadedWeapon()
        {
            if (loadedWeaponIndex < Weapons.Length)
            {
                return Weapons[loadedWeaponIndex];
            }
            return null;
        }

        public void Update(Player player)
        {
            for (var i = 0; i < Weapons.Length; i++)
            {
                Weapons[i].Update(player, i == loadedWeaponIndex);
            }
        }

        public void ChangeWeapon() => loadedWeaponIndex = (loadedWeaponIndex + 1) % Weapons.Length;

        public void ChangeTargetAttributeOfLoadedWeapon() => Weapons[loadedWeaponIndex].ChangeTargetAttribute();

        public void PullTriggerOfLoadedWeapon(Player player, BattleScene battleScene) => Weapons[loadedWeaponIndex].PullTrigger(player, battleScene);
    }
}
