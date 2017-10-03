using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdaptToGround : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // NavMeshHit hit;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        //if (NavMesh.Raycast(transform.position,Vector3.down,out hit,0))
        //{
        //    transform.up = hit.normal;
        //    Debug.Log("Normal: " + hit.normal);
        //}

        NavMeshHit navmeshHit;
        int walkableMask = NavMesh.AllAreas;
        if (NavMesh.SamplePosition(transform.position, out navmeshHit, 10.0f, walkableMask))
        {
            //Agent.SetDestination(navmeshHit.position);
            transform.up = navmeshHit.normal;
            Debug.Log("Normal: " + navmeshHit.normal);
        }


    }
}
