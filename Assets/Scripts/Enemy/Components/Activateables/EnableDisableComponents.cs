using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnableDisableComponents  : MonoBehaviour, IActivateable {

    [SerializeField]
    bool enable = true;

    [SerializeField]
    MonoBehaviour[] targets;

    public virtual void Activate(ActivateableState state = ActivateableState.NONE)
    {
        foreach(var t in targets)
        {
            t.enabled = enable;
        }
    }
    
}
