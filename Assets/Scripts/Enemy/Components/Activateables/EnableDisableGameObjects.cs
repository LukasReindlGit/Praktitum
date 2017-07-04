using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGameObjects : MonoBehaviour, IActivateable {
    [SerializeField]
    bool enable = true;

    [SerializeField]
    GameObject[] targets;

    public virtual void Activate(ActivateableState state = ActivateableState.NONE)
    {
        foreach (var g in targets)
        {
            g.SetActive( enable);
        }
    }

}
