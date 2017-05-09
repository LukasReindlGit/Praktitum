using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSound : MonoBehaviour
    {
        
        private WeaponBehaviour parentWeapon;

        [SerializeField]
        private AudioClip shootSound;

        private void OnEnable()
        {
            parentWeapon = GetComponent<WeaponBehaviour>();
            parentWeapon.FiredShot += ParentWeapon_FiredShot;

        }

        private void OnDisable()
        {
            parentWeapon.FiredShot -= ParentWeapon_FiredShot;
        }

        private void ParentWeapon_FiredShot(WeaponBehaviour weapon)
        {
            StartCoroutine(InstantiateSound(shootSound));
        }
        

        private IEnumerator InstantiateSound(AudioClip clip)
        {
            GameObject g = new GameObject();
            g.transform.position = transform.position;
            AudioSource auSou = g.AddComponent<AudioSource>();
            auSou.PlayOneShot(clip);

            while (auSou.isPlaying)
            {
                yield return new WaitForSeconds(1);
            }
            Destroy(g);
        }
    }
}