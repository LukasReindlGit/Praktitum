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

        private Transform targetColl;

        private void OnTriggerEnter(Collider other)
        {

            if (onlyOnce && activated)
            {
                return;
            }

            if (other.gameObject.tag == target)
            {
                targetColl = other.gameObject.transform;
                this.GetComponent<Lifecomponent>().SetTarget(other.gameObject);
                ActivateAllTargets();

                //((IActivateable) activateableTarget).Activate();
                activated = true;
            }
        }

        override protected void ActivateAllTargets()
        {
            //base.ActivateAllTargets();
            foreach (var a in activateables)
            {
                if (a is IUsesTarget)
                {
                    IUsesTarget iu = a as IUsesTarget;
                    iu.SetTarget(targetColl.transform);
                }
                ActivateTarget(a);
                
            }
            
        }
        //    virtual protected void ActivateAllTargets()
        //    {
        //        foreach (var a in activateables)
        //        {
        //            if(a is IUsesTarget)
        //            {
        //                IUsesTarget iu = a as IUsesTarget;
        //                iu.SetTarget(targetColl.transform);

        //            }
        //            IActivateable ia = a as IActivateable;
        //            if (ia != null)
        //            {
        //                ia.Activate();
        //            }
        //        }
        //    }
        }
    }