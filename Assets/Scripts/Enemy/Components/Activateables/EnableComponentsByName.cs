using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnableComponentsByName : MonoBehaviour, IActivateable
{

    [SerializeField]
    bool enable = true;

    [SerializeField]
    String[] targets;

    private MonoBehaviour[] targetsM;
    private void Start()
    {
        //search components that are called like in targets and put them in targetsM
        //NEVERMIND, lassen wir das, aber wir machen eine die disabled und enabled auf einmal

    }

    public virtual void Activate(ActivateableState state = ActivateableState.NONE)
    {
        foreach (var t in targetsM)
        {
            t.enabled = enable;
        }
    }

}
