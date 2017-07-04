using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponProjectileBehaviour : WeaponBehaviour
    {

        public GameObject projectile;


        public override void PerformShoot()
        {
            SpawnProjectile();

            // If you want the projectile to be accelerated by the weapon, use the WeaponAcceleratedProjectile.cs
        }

        protected GameObject SpawnProjectile()
        {
            return Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}