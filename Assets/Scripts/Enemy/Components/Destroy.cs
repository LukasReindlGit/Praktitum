using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour, IActivateable {
    
    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        Destroy(gameObject);
    }
}
