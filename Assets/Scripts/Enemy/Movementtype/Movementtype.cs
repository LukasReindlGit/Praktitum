using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Movementtype : MonoBehaviour {

    protected NavMeshAgent agent;
    protected bool active = false;

    public void goTo(Transform target)
    {
    agent.SetDestination(target.transform.position);
    }

    //muss in update aufgerufen werden
    public void goToRange(Transform target, float distance)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            StopMoving();
            return;
        }
        goTo(target);
    }

    public void StopMoving()
    {
        agent.SetDestination(agent.transform.position);
    }

    public void Activate()
    {
        active = !active;
    }

    

}
