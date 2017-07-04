using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWhenSeeTarget : MonoBehaviour 
{

    public event DefaultDelegate SeenPlayer;

    public GameObject toSpawn;
    public float timeBetweenChecks = 1;
    public float viewDistance = 10;
    public Transform target;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(SlowUpdate());
    }
    

    IEnumerator SlowUpdate()
    {
        while (true)
        {
            Check();
            yield return new WaitForSeconds(timeBetweenChecks);
        }
    }

    private void Check()
    {
        Ray ray = new Ray(transform.position, transform.forward * viewDistance);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, viewDistance))
        {
            if(hit.transform == target)
            {
                if (SeenPlayer != null) SeenPlayer.Invoke();

                GameObject g =  Instantiate(toSpawn, transform.position, transform.rotation);
                IUsesTarget[] inters = g.GetComponents<IUsesTarget>();
                foreach(var t in inters)
                {
                    t.SetTarget(target);
                }
            }
        }
    }
    
}
