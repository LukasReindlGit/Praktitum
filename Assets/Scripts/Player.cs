using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Weapon[] weapons;
    public int currentWeapon;
    public GameObject currentWeaponObject;

    /// <summary>
    /// Called by input manager to shoot
    /// </summary>
    public void HandleShootInput()
    {
        if (weapons[currentWeapon] == null)
            return;

        weapons[currentWeapon].TryShoot(transform.forward);
    }

    /// <summary>
    /// Called by input manager to switch weapons
    /// </summary>
    public void HandleNextWeaponInput()
    {
        weapons[currentWeapon].Despawn(this);
        currentWeapon = (currentWeapon + 1) % weapons.Length;
        weapons[currentWeapon].Spawn(this);
    }


    private void Update()
    {
        // Also updates selected targets
        currentWeapon.Update();
    }

}
