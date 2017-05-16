using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollow : MonoBehaviour {

    [SerializeField]
    Transform target;

    [SerializeField]
    float minDist = 1;

    NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < minDist)
        {
            agent.SetDestination(agent.transform.position);
            return;
        }

        agent.SetDestination(target.transform.position);
    }
}
