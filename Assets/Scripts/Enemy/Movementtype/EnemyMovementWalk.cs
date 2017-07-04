using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementWalk : Movementtype {

    [SerializeField]
    Transform target;

    [SerializeField]
    float speed;

    [SerializeField]
    float minDist = 0.5f;

    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    // Update is called once per frame
    void Update () {
		
	}

    //für den Fall dass speed zur Laufzeit geändert wird
    public float Speed
    {
        get
        {
            return agent.speed;
        }

        set
        {
            agent.speed = value;
        }
    }
}
