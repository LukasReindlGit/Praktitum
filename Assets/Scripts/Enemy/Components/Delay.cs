using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{
    public class Delay : MonoBehaviour, IActivateable
    {
        #region Variables

        [SerializeField]
        bool autostart = false;

        [SerializeField]
        float delayTime = 1;

        [SerializeField]
        List<MonoBehaviour> activateables = new List<MonoBehaviour>();

        [SerializeField]
        bool loop = false;

        bool running = false;
        #endregion

        #region Methods

        private void Start()
        {
            if(autostart)
            {
                Activate();
            }
        }

        IEnumerator WaitThenFire()
        {
            // Only one instance should be running at the time
            if (running)
            {
                yield return null;
            }

            running = true;

            do
            {
                // Wait the defined amount of time
                yield return new WaitForSeconds(delayTime);

                // Activate all targets
                foreach (var a in activateables)
                {
                    IActivateable ia = (IActivateable) a;
                    if (ia != null)
                    {
                        ia.Activate();
                    }
                }
            } while (loop);

            running = false;
        }

        /// <summary>
        /// Called from outside
        /// </summary>
        /// <param name="state"></param>
        public void Activate(ActivateableState state = ActivateableState.NONE)
        {
            StartCoroutine(WaitThenFire());
        }
        #endregion
    }
}