using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapons
{
    public class RaycastBehaviour : WeaponBehaviour
    {
        
        public override void PerformShoot()
        {
            ShootDamagingRay(transform.position, transform.forward);
        }
        
        private void ShootDamagingRay(Vector3 origin, Vector3 direction)
        {
            RaycastHit hit;
            Ray ray = new Ray(origin, direction * param.range);
            if (Physics.Raycast(ray, out hit))
            {
                IDamageable damagableTarget = hit.transform.GetComponent<IDamageable>();
                if (damagableTarget != null)
                {
                    damagableTarget.DoDamage(param.damage);
                }
            }

            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
        }
    }
}