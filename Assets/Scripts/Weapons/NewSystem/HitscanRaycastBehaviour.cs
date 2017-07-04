using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class HitscanRaycastBehaviour : RaycastBehaviour
    {
        private TargetingSystem targetingSystem;

        List<Target> availableTargets;

        public override void Init()
        {
            base.Init();
            targetingSystem = new HitScanTargeting(this);
            StartedSalve += UpdateTargets;
        }

        private void OnDestroy()
        {
            StartedSalve -= UpdateTargets;
        }

        private void UpdateTargets(WeaponBehaviour weapon)
        {
            if (availableTargets == null)
                return;

            availableTargets.Clear();

            foreach (var a in targetingSystem.GetTargets(transform.position, transform.forward, param))
            {
                availableTargets.Add(a);
            }

        }

        public override void PerformShoot()
        {
            if (availableTargets == null || availableTargets.Count <= 0)
            {
                // Default shot straight forward
                ShootDamagingRay(transform.position, transform.forward);
            }
            else
            {
                // Shoot at first target in list.

                Vector3 direction = (availableTargets[0].TargetPos - transform.position).normalized;
                ShootDamagingRay(transform.position, direction);

                availableTargets.RemoveAt(0);
            }
        }
    }
}