using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformCamAnimOnShot : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GetComponent<Weapons.WeaponBehaviour>().FiredShot += PerformCamAnimOnShot_FiredShot;	
	}

    private void PerformCamAnimOnShot_FiredShot(Weapons.WeaponBehaviour weapon)
    {
        FindObjectOfType<CameraWeaponEffect>().CallEffect();
    }
    
}
