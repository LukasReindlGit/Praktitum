using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour , IActivateable {

    [SerializeField]
    float delayTime = 1;

    [SerializeField]
    List<MonoBehaviour> activateables = new List<MonoBehaviour>();
    
    IEnumerator WaitThenFire()
    {
        yield return new WaitForSeconds(delayTime);

        foreach(var a in activateables)
        {
            IActivateable ia = a.GetComponent<IActivateable>();
            if (ia!=null)
            {
                ia.Activate();
            }
        }
    }
    
    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        StartCoroutine(WaitThenFire());
    }
}
