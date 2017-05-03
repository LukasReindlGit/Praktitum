using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputHandler : MonoBehaviour {
    
    public Player pawnPlayer;
    
    public KeyCode fireKey = KeyCode.Return;
    public KeyCode changeWeapon = KeyCode.Q;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(fireKey))
        {
            pawnPlayer.HandleShootInput();
        }

        if(Input.GetKeyDown(changeWeapon))
        {
            pawnPlayer.HandleNextWeaponInput();
        }
    }
}
