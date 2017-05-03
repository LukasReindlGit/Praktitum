using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class HitscanRaycastBehaviour : RaycastBehaviour
    {
        private TargetingSystem targetingSystem;

        public override void Init()
        {
            base.Init();
            targetingSystem = new TestTargetingSystem();
            StartedSalve += UpdateTargets;
        }

        private void OnDestroy()
        {
            StartedSalve -= UpdateTargets;
        }

        private void UpdateTargets(WeaponBehaviour weapon)
        {
            throw new System.NotImplementedException();
        }

        public override void PerformShoot()
        {
            
            // TODO: werte in param übernehmen. get target über salvenevent holen.
            targetingSystem.GetTargets(transform.forward, 0.5f, 0, param.salveCount, 1);
        }
    }
}