﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSpawnObjectOnShot : MonoBehaviour
    {
        private WeaponBehaviour parentWeapon;

        [SerializeField]
        private GameObject[] ObjectsToSpawn;

        [SerializeField]
        private bool spawnAsChild = false;

        private void OnEnable()
        {
            parentWeapon = GetComponent<WeaponBehaviour>();
            parentWeapon.FiredShot += SpawnObjectAtFire;
        }

        private void OnDisable()
        {
            parentWeapon.FiredShot -= SpawnObjectAtFire;
        }

        private void SpawnObjectAtFire(WeaponBehaviour weapon)
        {
            foreach (GameObject objectToSpawn in ObjectsToSpawn)
            {
                GameObject g = Instantiate(objectToSpawn, transform.position, transform.rotation);
                if (spawnAsChild)
                {
                    g.transform.parent = transform;
                }
            }
        }

    }
}
