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

        public GameObject targetPlayer;
        public float requiredRange = 30;

        private Transform targetColl;
        private Lifecomponent lifecomp;
        private bool isInRange = false;

        public void Start()
        {
            lifecomp = this.GetComponent<Lifecomponent>();
            targetPlayer = lifecomp.target;
        }

        

        public void FixedUpdate()
        {
            if (targetPlayer != null)
            {
                if (!isInRange && Vector3.Distance(transform.position, targetPlayer.transform.position) <= requiredRange)
                {
                    targetColl = targetPlayer.gameObject.transform;
                    lifecomp.SetTarget(targetPlayer.gameObject);
                    ActivateAllTargets();
                    isInRange = true;

                    activated = true;
                }
                else if (isInRange && Vector3.Distance(transform.position, targetPlayer.transform.position) > requiredRange)
                {
                    isInRange = false;
                }
            }
        }

        

        private void OnTriggerEnter(Collider other)
        {

            if (onlyOnce && activated)
            {
                return;
            }

            if (other.gameObject.tag == target)
            {
                targetColl = other.gameObject.transform;
                lifecomp.SetTarget(other.gameObject);
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