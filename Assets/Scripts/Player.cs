using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject[] weapons;
    public int currentWeaponIndex;
    public GameObject currentWeaponObject;

    public void Awake()
    {
        GameHandler.players.Add(this);
        SpawnCurrentWeapon();
    }

    public void OnDestroy()
    {
        GameHandler.players.Remove(this);
    }

    /// <summary>
    /// Called by input manager to shoot
    /// </summary>
    public void HandleShootInput()
    {
        if (weapons[currentWeaponIndex] == null)
            return;

        weapons[currentWeaponIndex].GetComponent<Weapon>().TryShoot(transform.forward);
    }

    /// <summary>
    /// Called by input manager to switch weapons
    /// </summary>
    public void HandleNextWeaponInput()
    {
        NextWeapon();
    }

    private void NextWeapon()
    {
        Destroy(currentWeaponObject);
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

        // Spawn weapon
        SpawnCurrentWeapon();
    }

    private void SpawnCurrentWeapon()
    {
        currentWeaponObject = Instantiate(weapons[currentWeaponIndex]) as GameObject;
    }
}
