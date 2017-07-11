using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateConstantly : MonoBehaviour {

    [SerializeField]
    Vector3 axis = Vector3.up;

    [SerializeField]
    float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(axis * speed * Time.deltaTime, Space.Self);
	}
}
