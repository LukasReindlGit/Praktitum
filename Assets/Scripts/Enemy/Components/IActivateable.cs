using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivateableState {NONE };

public interface IActivateable {

    void Activate(ActivateableState state = ActivateableState.NONE);
    
         
}
