using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Slerp(transform.position, target.transform.position + offset,Time.deltaTime*speed);
	}
}
