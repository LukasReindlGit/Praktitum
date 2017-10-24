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
                    setNewDestination(wanderRadius);
                    timer = 0;
                }
                return;
            }

            if (Vector3.Distance(transform.position, agent.destination) <= Dist)
            {
                setNewDestination(wanderRadius);
            }
        }


    }
    
}
