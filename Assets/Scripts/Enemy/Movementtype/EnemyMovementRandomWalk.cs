using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementRandomWalk : EnemyMovementWalk {

    //ist randomwalk von timer abhängig
    [SerializeField]
    bool byTime;

    [SerializeField]
    float wanderTimer;

    [SerializeField]
    float wanderRadius;

    private float timer;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        timer = wanderTimer;
        target = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (byTime)
            {
                timer += Time.deltaTime;

                if (timer >= wanderTimer)
                {
                    setNewDestination();
                    timer = 0;
                }
                return;
            }

            if (Vector3.Distance(transform.position, agent.destination) <= Dist)
            {
                setNewDestination();
            }
        }


    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void setNewDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
        agent.SetDestination(newPos);
    }
}
