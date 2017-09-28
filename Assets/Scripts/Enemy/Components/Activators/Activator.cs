using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Component
{

    public abstract class Activator : MonoBehaviour
    {
        [SerializeField]
        protected MonoBehaviour[] activateables;

        [SerializeField]
        protected bool onlyOnce = false;
        protected bool activated = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        virtual protected void ActivateAllTargets()
        {
            foreach (var a in activateables)
            {
                ActivateTarget(a);
            }
        }

        protected void ActivateTarget(MonoBehaviour a)
        {
            IActivateable ia = a as IActivateable;
            if (ia != null)
            {
                ia.Activate();
            }
        }

    }
}