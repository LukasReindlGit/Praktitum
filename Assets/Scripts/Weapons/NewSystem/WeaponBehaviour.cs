using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        public delegate void WeaponEvent(WeaponBehaviour weapon);

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

        #region Events
        public event WeaponEvent StartedLoading;
        public event WeaponEvent FinishedLoading;
        public event WeaponEvent AbortedLoading;

        public event WeaponEvent StartedSalve;
        public event WeaponEvent FinishedSalve;

        public event WeaponEvent FiredShot;

        public event WeaponEvent StartedCooldown;
        public event WeaponEvent FinishedCooldown;

        public event WeaponEvent StartedReload;
        public event WeaponEvent FinishedReload;

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

            do
            {
                #region Load the shot

                // Fire event StartedLoading
                if (StartedLoading != null) StartedLoading.Invoke(this);

                // Wait loadingTime
                yield return new WaitForSeconds(param.loadingTime);

                // Fire event FinishedLoading
                if (FinishedLoading != null) FinishedLoading.Invoke(this);

                readyToShoot = false;
                #endregion

                // Check if we aborted the loading by releasing or repressing
                if (!isPressingButton || timeStampLastTryShot != timeStamp)
                {
                    // Fire event Aborted
                    if (AbortedLoading != null) AbortedLoading.Invoke(this);
                    yield break;
                }
                isWaitingForRelease = true;

                // Shoot each shot of the salve
                for (int i = param.salveCount; i > 0; i--)
                {
                    // Fire event FiredShot
                    if (FiredShot != null) FiredShot.Invoke(this);
                    PerformShoot();

                    // Fire event StartedCooldown
                    if (StartedCooldown != null) StartedCooldown.Invoke(this);
                    
                    // Wait the cooldown time
                    yield return new WaitForSeconds(param.cooldownTime);

                    // Fire event FinishedCooldown
                    if (FinishedCooldown != null) FinishedCooldown.Invoke(this);
                }

                // Use ammo from the clip
                currentClip--;

                // Reload if needed
                if (currentClip <= 0)
                {

                    // Fire event StartedReload
                    if (StartedReload != null) StartedReload.Invoke(this);

                    yield return new WaitForSeconds(param.reloadTime);
                    currentClip = param.clipSize;

                    // Fire event FinishedReload
                    if (FinishedReload != null) FinishedReload.Invoke(this);
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