using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Component;
using System;

public class ActivateableActivator : AI.Component.Activator, IActivateable {

    [SerializeField]
    protected bool active = false;

    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        ActivateAllTargets();
        if (onlyOnce)
        {
            this.enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
