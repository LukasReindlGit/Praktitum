using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Movementtype : MonoBehaviour {

    protected NavMeshAgent agent;

    [SerializeField]
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

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    protected void setNewDestination(float radius)
    {
        Vector3 newPos = RandomNavSphere(transform.position, radius, NavMesh.AllAreas);
        agent.SetDestination(newPos);
    }


}
