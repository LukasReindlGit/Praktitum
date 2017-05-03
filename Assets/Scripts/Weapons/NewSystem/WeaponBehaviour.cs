using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
      
        #region Variables

        public KeyCode debugShootKey = KeyCode.Return;

        /// <summary>
        /// Contains all the information about the weapon
        /// </summary>
        public Parameters param;

        protected bool isPressingButton;
        protected bool isWaitingForRelease = false;
        
        protected int currentClip = 0;
        private bool readyToShoot = true;

        private float timeStampLastTryShot;

        #endregion

        #region Functions

        public virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            currentClip = param.clipSize;
            readyToShoot = true;
        }

        public virtual void Update()
        {
            if (Input.GetKeyDown(debugShootKey))
            {
                isPressingButton = true;
                TryShoot();
            }

            if (Input.GetKeyUp(debugShootKey))
            {
                isPressingButton = false;
                isWaitingForRelease = false;
            }
        }

        private void TryShoot()
        {
            Debug.Log("Try Shoot");
            if (readyToShoot || true)
            {
                timeStampLastTryShot = Time.time;
                StartCoroutine(ShootLoop(timeStampLastTryShot));
            }
        }

        IEnumerator ShootLoop(float timeStamp)
        {

            Debug.Log("Starting Coroutine");
            do
            {
                Debug.Log("LOADING");
                // Load the shot
                yield return new WaitForSeconds(param.loadingTime);

                readyToShoot = false;

                // Check if we aborted the loading by releasing or repressing
                if (!isPressingButton || timeStampLastTryShot != timeStamp)
                    yield break;

                isWaitingForRelease = true;

                // Shoot each shot of the salve
                for (int i = param.salveCount; i > 0; i--)
                {
                    Debug.Log("SHOT!");
                    PerformShoot();
                    yield return new WaitForSeconds(param.cooldownTime);
                }

                // Use ammo from the clip
                currentClip--;

                // Reload if needed
                if (currentClip <= 0)
                {

                    Debug.Log("Reloading");
                    yield return new WaitForSeconds(param.reloadTime);
                    currentClip = param.clipSize;
                }


                Debug.Log("Repeat");

                // if we have a continuous fire weapon, Continue shooting until we release the button.
            } while (param.isContinuousFire && isPressingButton);

            readyToShoot = true;
        }

        public abstract void PerformShoot();

        #endregion
    }
}