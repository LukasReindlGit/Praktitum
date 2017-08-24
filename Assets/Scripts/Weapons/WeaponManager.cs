using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {

        [SerializeField]
        WeaponBehaviour[] weapons;

        [SerializeField]
        KeyCode shootKey;

        [SerializeField]
        KeyCode nextWeaponKey;

        [SerializeField]
        KeyCode previousWeaponKey;

        [SerializeField]
        Vector3 positionOffset= Vector3.zero;

        WeaponBehaviour currentWeapon;
        [SerializeField]
        int currentIndex = 0;

        private void Start()
        {
            UpdateWeapon();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(nextWeaponKey))
            {
                TryChangeWeapon(true);
            }
            else
            if (Input.GetKeyDown(previousWeaponKey))
            {
                TryChangeWeapon(false);
            }
            if (Input.GetKeyDown(shootKey))
            {
                currentWeapon.SetKeyPressed(true);
            }

            if (Input.GetKeyUp(shootKey))
            {
                currentWeapon.SetKeyPressed(false);
            }
        }

        private void TryChangeWeapon(bool Next)
        {
            if (currentWeapon.State == WeaponBehaviour.WeaponState.RELOADING)
            {
                return;
            }

            if (Next)
            {
                NextWeapon();
            }
            else
            {
                PreviousWeapon();
            }
        }

        private void NextWeapon()
        {
            currentIndex = (currentIndex + 1) % weapons.Length;
            UpdateWeapon();
        }

        private void PreviousWeapon()
        {
            currentIndex = (currentIndex - 1 + weapons.Length) % weapons.Length;
            UpdateWeapon();
        }

        private void UpdateWeapon()
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            GameObject g = Instantiate(weapons[currentIndex].gameObject, transform);
            g.transform.localPosition = positionOffset;
            currentWeapon = g.GetComponent<WeaponBehaviour>();
        }
    }
}