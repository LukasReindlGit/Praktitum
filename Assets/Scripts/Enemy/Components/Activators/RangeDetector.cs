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
        GameObject activateablesObject;

        List<MonoBehaviour> activateables = new List<MonoBehaviour>();

        //[SerializeField]
        //MonoBehaviour activateableTarget;

        [SerializeField]
        bool onlyOnce = false;
        bool activated = false;

        void Start()
        {
            IActivateable[] activ = FindObjectsOfType(typeof(IActivateable)) as IActivateable[];
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("look guests");

            if (onlyOnce && activated)
            {
                Debug.Log("ups");
                return;
            }

            if (other.gameObject == target)
            {
                
                // Activate all targets
                foreach (var a in activateables)
                {
                    IActivateable ia = (IActivateable)a;
                    if (ia != null)
                    {
                        ia.Activate();
                    }
                }
                
                //((IActivateable) activateableTarget).Activate();
                activated = true;
            }
        }
    }
}