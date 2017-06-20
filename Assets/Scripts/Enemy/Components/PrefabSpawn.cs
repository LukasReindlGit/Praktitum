using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawn : MonoBehaviour, IActivateable {

    [SerializeField]
    GameObject objectToSpawn;

    [SerializeField]
    bool spawnAsChild = false;

    [SerializeField]
    Vector3 positionOffset;

    private void SpawnObject()
    {
        GameObject g = Instantiate(objectToSpawn);
        if (spawnAsChild)
        {
            g.transform.parent = transform;
        }

        g.transform.position = transform.position + positionOffset;
        g.transform.rotation = transform.rotation;
    }

    /// <summary>
    /// Called by activators
    /// </summary>
    /// <param name="state"></param>
    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        SpawnObject();
    }
}
