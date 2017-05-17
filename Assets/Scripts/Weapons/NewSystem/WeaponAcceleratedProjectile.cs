using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponAcceleratedProjectile : WeaponProjectileBehaviour
    {
        public float projectileVelocity = 5;

        public override void PerformShoot()
        {
            GameObject proj = SpawnProjectile();
            AccelerateProjectile(proj);
        }

        private void AccelerateProjectile(GameObject proj)
        {
            Rigidbody r = proj.GetComponent<Rigidbody>();
            if(r!=null)
            {
                r.velocity = Vector3.forward * projectileVelocity;
            }
        }
    }
}