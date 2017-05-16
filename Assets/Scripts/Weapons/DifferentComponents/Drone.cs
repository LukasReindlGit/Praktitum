using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drone : MonoBehaviour {

    [SerializeField]
    float forcePerSecond = 10;

    Rigidbody rigid;

    [SerializeField]
    Transform target;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(target)
        {
            transform.LookAt(target.transform.position);
        }

        rigid.AddForce(transform.forward * forcePerSecond * Time.fixedDeltaTime);
        
    }
}
