using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract class Movement : MonoBehaviour {

    NavMeshAgent agent;

    public void goTo(Transform target)
    {
    agent.SetDestination(agent.transform.position);
    }

    public void goToRange(Transform target, float maxDistance)
    {
     
    }
	
}
