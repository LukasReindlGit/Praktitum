using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class HitscanRaycastBehaviour : RaycastBehaviour
    {
        private TargetingSystem targetingSystem;

        List<Target> availableTargets= new List<Target>();

        public override void Init()
        {
            base.Init();
            targetingSystem = new HitScanTargeting(this);
            StartedSalve += UpdateTargets;
        }

        private void FixedUpdate()
        {
            targetingSystem.UpdateTargetSystem(transform.position, transform.forward);
        }

        private void OnDestroy()
        {
            StartedSalve -= UpdateTargets;
        }

        private void UpdateTargets(WeaponBehaviour weapon)
        {
            availableTargets.Clear();

            foreach (var a in targetingSystem.GetTargets(transform.position, transform.forward, param))
            {
                availableTargets.Add(a);
                Debug.Log("Adding target: " + a.TargetObject.name);
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
                Quaternion tempRot = transform.rotation;
                Vector3 direction = (availableTargets[0].TargetPos - transform.position).normalized;
                transform.LookAt(availableTargets[0].TargetPos);
                ShootDamagingRay(transform.position, direction);
                transform.rotation = tempRot;
                availableTargets.RemoveAt(0);
            }
        }
    }
}