#nullable enable
namespace Weapons
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class WeaponsHolder
    {
        public IWeapon[] Weapons => weapons;
        [SerializeReference] private IWeapon[] weapons;
        private int loadedWeaponIndex = 0;

        public WeaponsHolder(IWeapon[] weapons)
        {
            this.weapons = weapons;
        }

        public IWeapon? GetLoadedWeapon()
        {
            if (loadedWeaponIndex < weapons.Length)
            {
                return weapons[loadedWeaponIndex];
            }
            return null;
        }

        public void Update(Player player)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].Update(player, i == loadedWeaponIndex);
            }
        }

        public void ChangeWeapon()
        {
            loadedWeaponIndex = (loadedWeaponIndex + 1) % weapons.Length;
        }

        public void ChangeTargetAttributeOfLoadedWeapon()
        {
            weapons[loadedWeaponIndex].ChangeTargetAttribute();
        }

        public void PullTriggerOfLoadedWeapon(Player player, BattleScene battleScene)
        {
            weapons[loadedWeaponIndex].PullTrigger(player, battleScene);
        }
    }
}
