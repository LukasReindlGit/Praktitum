using System;
using System.Collections;
using System.Collections.Generic;
using AI.Component;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementWalk : Movementtype {

    [SerializeField]
    protected float speed = 10;

    [SerializeField]
    private float dist = 1.5f;


    // Use this for initialization
    virtual protected void Start()
    {
        //Activate();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update ()
    {
        //wenn deaktiviert läuft er zum letzten übergebenen Punkt
        if (active)
        {
            goToRange(target, dist);
            return;
        }		
	}

    //für den Fall dass speed or dist zur Laufzeit geändert wird
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

    protected float Dist
    {
        get
        {
            return dist;
        }

        set
        {
            dist = value;
        }
    }
}
