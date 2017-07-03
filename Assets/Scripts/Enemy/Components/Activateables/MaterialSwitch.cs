using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MaterialSwitch : MonoBehaviour, IActivateable {

    [SerializeField]
    Material[] materials;

    [SerializeField]
    int index = 0;

    [SerializeField]
    Renderer[] renderers;

    [SerializeField]
    bool loop = true;

    private void Start()
    {
        UpdateMaterial();
    }

    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        IncreaseIndex();
        UpdateMaterial();
    }

    private void IncreaseIndex()
    {
        index = index + 1;
        if (loop)
        {
            index = index % materials.Length;
        }
        else
        {
            if(index>=materials.Length)
            {
                index = materials.Length - 1;
            }
        }
    }

    private void UpdateMaterial()
    {
        foreach(Renderer r in renderers)
        {
            r.material = materials[index];
        }
    }
}
