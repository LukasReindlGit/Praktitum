using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{
    [RequireComponent(typeof(SphereCollider))]
    public class RangeDetector : Activator
    {
        [SerializeField]
        GameObject target;

        [SerializeField]
        MonoBehaviour activateableTarget;

        [SerializeField]
        bool onlyOnce = false;
        bool activated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (onlyOnce && activated)
            {
                return;
            }

            if (other.gameObject == target)
            {
                ((IActivateable) activateableTarget).Activate();
                activated = true;
            }
        }
    }
}