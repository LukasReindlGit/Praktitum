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


        private void OnTriggerEnter(Collider other)
        {

            if (onlyOnce && activated)
            {
                Debug.Log("ups");
                return;
            }

            if (other.gameObject.tag == target)
            {
                ActivateAllTargets();

                //((IActivateable) activateableTarget).Activate();
                activated = true;
            }
        }

        private void ActivateAllTargets()
        {
            foreach (var a in activateables)
            {
                IActivateable ia = a as IActivateable;
                if (ia != null)
                {
                    ia.Activate();
                }
            }
        }
    }
}