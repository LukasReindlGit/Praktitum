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
        
        [SerializeField]
        private AudioClip salveSound;

        [SerializeField]
        private AudioClip reloadSound;
        

        private void OnEnable()
        {
            parentWeapon = GetComponent<WeaponBehaviour>();
            parentWeapon.FiredShot += ParentWeapon_FiredShot;
            parentWeapon.StartedSalve += ParentWeapon_StartedSalve; ;
            parentWeapon.StartedReload += ParentWeapon_StartedReload;

        }
        
        private void OnDisable()
        {
            parentWeapon.FiredShot -= ParentWeapon_FiredShot;
            parentWeapon.StartedSalve -= ParentWeapon_StartedSalve; ;
            parentWeapon.StartedReload -= ParentWeapon_StartedReload;
        }

        private void ParentWeapon_StartedSalve(WeaponBehaviour weapon)
        {
            StartCoroutine(InstantiateSound(salveSound));
        }

        private void ParentWeapon_StartedReload(WeaponBehaviour weapon)
        {
            StartCoroutine(InstantiateSound(reloadSound));
        }

        private void ParentWeapon_FiredShot(WeaponBehaviour weapon)
        {
            StartCoroutine(InstantiateSound(shootSound));
        }
        

        private IEnumerator InstantiateSound(AudioClip clip)
        {
            if(clip==null)
            {
                yield return null;
            }

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