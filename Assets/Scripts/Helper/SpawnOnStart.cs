using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this to be able to have prefabs inside prefabs
/// </summary>
public class SpawnOnStart : MonoBehaviour {

    enum Method { AWAKE,START}


    [SerializeField]
    Method calledOn = Method.AWAKE;
    public GameObject toSpawn;

    [SerializeField]
    private bool asChild = true;

    
	void Awake ()
    {
        if (calledOn == Method.AWAKE)
        {
            Spawn();
        }
    }

    private void Start()
    {
        if(calledOn == Method.START)
        {
            Spawn();
        }
    }

    /// <summary>
    ///  Performs the spawn of the object
    /// </summary>
    private void Spawn()
    {
        GameObject g = Instantiate(toSpawn, transform.position, transform.rotation);

        if (asChild)
        {
            g.transform.parent = transform;
        }
    }
}
