using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAI : MonoBehaviour, IActivateable {

    public UnityEvent test;

    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
