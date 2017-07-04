using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.Component
{
    public class AISound : MonoBehaviour, IActivateable
    {
        #region Variables

        [SerializeField]
        AudioClip clip;

        #endregion

        public void Activate(ActivateableState state = ActivateableState.NONE)
        {
            //search audioSource or create one if not found
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource.Equals(null))
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            //set and play clip
            audioSource.clip = clip;
            audioSource.Play();
            
        }

    }
}