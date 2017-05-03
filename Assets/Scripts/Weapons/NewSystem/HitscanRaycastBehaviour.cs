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
        }

        public override void PerformShoot()
        {
            // TODO: werte in param übernehmen. get target über salvenevent holen.
            targetingSystem.GetTargets(transform.forward, 0.5f, 0, param.salveCount, 1);
        }
    }
}