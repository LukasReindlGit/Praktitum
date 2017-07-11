using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSpawnObjectOnShot : MonoBehaviour
    {
        private WeaponBehaviour parentWeapon;

        [SerializeField]
        private GameObject ObjectToSpawn;

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
            GameObject g = Instantiate(ObjectToSpawn, transform.position,transform.rotation);
            if(spawnAsChild)
            {
                g.transform.parent = transform;
            }
        }

    }
}
