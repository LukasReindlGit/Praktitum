using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementNavMesh : MonoBehaviour {

    public float speed = 80;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //pawn.transform.Translate(new Vector3(x, 0, y) * Time.deltaTime * pawnSpeed, Space.World);
        agent.SetDestination(transform.position + new Vector3(x, 0, y) * Time.deltaTime * speed);
    }
}
