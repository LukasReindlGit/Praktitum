using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        public enum WeaponState { IDLE, LOADING, COOLDOWN, RELOADING }

        public delegate void WeaponEvent(WeaponBehaviour weapon);
        public delegate void WeaponHitEvent(WeaponBehaviour weapon, RaycastHit hit, Vector3 cameFromDir);
        public delegate void WeaponShotEvent(WeaponBehaviour weapon, Vector3 origin, Vector3 direction);

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

        private Quaternion rotationBackup;
        
        private WeaponState state = WeaponState.IDLE;
        public WeaponState State { get { return state; } }

        #endregion

        #region Events
        public event WeaponEvent StartedLoading;
        public event WeaponEvent FinishedLoading;
        public event WeaponEvent AbortedLoading;

        public event WeaponEvent StartedSalve;
        public event WeaponEvent FinishedSalve;
        
        public event WeaponEvent FiredShot;
        public event WeaponHitEvent HitSomething;

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
            currentClip = param.ClipSize;
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
                //isWaitingForRelease = false;
                TryAbort();
            }
        }

        private void TryAbort()
        {
            if(state == WeaponState.LOADING || state == WeaponState.IDLE)
            {
                state = WeaponState.IDLE;
                StopAllCoroutines();
            }
        }

        private void TryShoot()
        {
            if (state == WeaponState.IDLE)
            {
                //timeStampLastTryShot = Time.time;
                StartCoroutine(ShootLoop(timeStampLastTryShot));
            }
        }

        IEnumerator ShootLoop(float timeStamp)
        {
            do
            {
                #region Load the shot

                // Fire event StartedLoading
                state = WeaponState.LOADING;
                if (StartedLoading != null) StartedLoading.Invoke(this);
                
                // Wait loadingTime
                yield return new WaitForSeconds(param.LoadingTime);

                // Fire event FinishedLoading
                state = WeaponState.IDLE;
                if (FinishedLoading != null) FinishedLoading.Invoke(this);

                readyToShoot = false;

                #endregion
                               
                if (StartedSalve != null) StartedSalve.Invoke(this);

                // Shoot each shot of the salve
                for (int i = param.SalveCount; i > 0; i--)
                {

                    rotationBackup = transform.rotation;
                    PerformShoot();
                    // Fire event FiredShot
                    if (FiredShot != null) FiredShot.Invoke(this);
                    transform.rotation = rotationBackup;

                    // Fire event StartedCooldown
                    if (StartedCooldown != null) StartedCooldown.Invoke(this);

                    // Wait the cooldown time
                    state = WeaponState.COOLDOWN;
                    yield return new WaitForSeconds(param.CooldownTime);

                    state = WeaponState.IDLE;
                    // Fire event FinishedCooldown
                    if (FinishedCooldown != null) FinishedCooldown.Invoke(this);
                }
                if (FinishedSalve != null) FinishedSalve.Invoke(this);

                // Use ammo from the clip
                currentClip--;

                // Reload if needed
                if (currentClip <= 0)
                {
                    // Fire event StartedReload
                    state = WeaponState.RELOADING;
                    if (StartedReload != null) StartedReload.Invoke(this);

                    yield return new WaitForSeconds(param.ReloadTime);
                    currentClip = param.ClipSize;

                    // Fire event FinishedReload
                    state = WeaponState.IDLE;
                    if (FinishedReload != null) FinishedReload.Invoke(this);
                }
                                
                // if we have a continuous fire weapon, Continue shooting until we release the button.
            } while (param.IsContinuousFire && isPressingButton);

            readyToShoot = true;
        }

        public abstract void PerformShoot();

        protected void CallHitEvent(RaycastHit hit, Vector3 shotOrigin)
        {
            if (HitSomething != null) HitSomething.Invoke(this,hit,shotOrigin);
        }
        #endregion
    }
}