using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class HitscanProjectileBehaviour : WeaponProjectileBehaviour
    {
        private TargetingSystem targetingSystem;

        [SerializeField]
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

        private void FixedUpdate()
        {
            targetingSystem.UpdateTargetSystem(transform.position, transform.forward);           
        }

        private void UpdateTargets(WeaponBehaviour weapon)
        {
            availableTargets.Clear();
            Target[] targetArr = targetingSystem.GetTargets(transform.position, transform.forward, param);
            if (targetArr == null)
                return;

            foreach (var a in targetArr)
            {
                availableTargets.Add(a);
            }
        }

        /// <summary>
        /// Perform Shot
        /// </summary>
        public override void PerformShoot()
        {
            if (availableTargets == null || availableTargets.Count <= 0)
            {
                // Default shot straight forward
                SpawnProjectile();
            }
            else
            {
                // Shoot at first target in list.

                Vector3 direction = (availableTargets[0].TargetPos - transform.position).normalized;
                GameObject g = SpawnProjectile();
                    g.transform.forward = direction;

                // Apply target to projectile if supported:
                IUsesTarget[] tArr = g.GetComponents<IUsesTarget>();
                foreach(var t in tArr)
                {
                    t.SetTarget(availableTargets[0].TargetObject.transform);
                }

                availableTargets.RemoveAt(0);
            }
        }
    }
}