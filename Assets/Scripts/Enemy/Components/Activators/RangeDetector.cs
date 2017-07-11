using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{
    [RequireComponent(typeof(SphereCollider))]
    public class RangeDetector : Activator
    {
        [SerializeField]
        String target;

        [SerializeField]
        MonoBehaviour[] activateables;
               
        [SerializeField]
        bool onlyOnce = false;
        bool activated = false;

        private Transform targetColl;

        private void OnTriggerEnter(Collider other)
        {

            if (onlyOnce && activated)
            {
                return;
            }

            if (other.gameObject.tag == target)
            {
                Debug.Log("Found player");
                targetColl = other.gameObject.transform;
                ActivateAllTargets();

                //((IActivateable) activateableTarget).Activate();
                activated = true;
            }
        }

        private void ActivateAllTargets()
        {
            foreach (var a in activateables)
            {
                if(a is IUsesTarget)
                {
                    IUsesTarget iu = a as IUsesTarget;
                    iu.SetTarget(targetColl.transform);

                }
                IActivateable ia = a as IActivateable;
                if (ia != null)
                {
                    ia.Activate();
                }
            }
        }
    }
}