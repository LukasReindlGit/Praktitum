using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSpawnObjectOnImpact : MonoBehaviour
    {

        private WeaponBehaviour parentWeapon;

        [SerializeField]
        private GameObject ObjectToSpawn;

        private void OnEnable()
        {
            parentWeapon = GetComponent<WeaponBehaviour>();
            parentWeapon.HitSomething += SpawnObjectAtImpact;

        }
        
        private void OnDisable()
        {
            parentWeapon.HitSomething -= SpawnObjectAtImpact;
        }
        
        private void SpawnObjectAtImpact(WeaponBehaviour weapon, RaycastHit hit, Vector3 origin)
        {
            GameObject g = Instantiate(ObjectToSpawn, hit.point, Quaternion.identity);
            g.transform.forward = (origin - hit.point).normalized;
        }

    }
}
