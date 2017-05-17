using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehaviour : MonoBehaviour {

    [Header("Basic Parameters")]

    public float viewAngle;
    public float distance;

    [Header("References")]
    public Transform characterCenter;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.rotation = Quaternion.Euler(new Vector3(90 - viewAngle, 0, 0)); 

        this.transform.position = characterCenter.position - (transform.forward * distance);

    }
}
