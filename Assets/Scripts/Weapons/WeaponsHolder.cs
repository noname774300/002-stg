#nullable enable

using UnityEngine;

[System.Serializable]
public class WeaponsHolder
{
    public IWeapon[] Weapons => weapons;
    [SerializeReference] private IWeapon[] weapons;
    private int loadedWeaponIndex = 0;

    public WeaponsHolder(IWeapon[] weapons) => this.weapons = weapons;

    public IWeapon? GetLoadedWeapon() => loadedWeaponIndex < Weapons.Length ? Weapons[loadedWeaponIndex] : null;

    public void Update(GameObject user, string targetTag)
    {
        for (var i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].Update(user, targetTag, i == loadedWeaponIndex);
        }
    }

    public void ChangeWeapon() => loadedWeaponIndex = (loadedWeaponIndex + 1) % Weapons.Length;

    public void ChangeTargetAttributeOfLoadedWeapon() => Weapons[loadedWeaponIndex].ChangeTargetAttribute();

    public void PullTriggerOfLoadedWeapon(GameObject user, BattleScene battleScene) => Weapons[loadedWeaponIndex].PullTrigger(user, battleScene);
}
