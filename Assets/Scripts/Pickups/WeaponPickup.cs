using UnityEngine;

public  class WeaponPickup : Pickup
{
    [SerializeField] WeaponSO weaponSO;
         

    protected override void OnAmmoPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapon(weaponSO);
    }   
}
