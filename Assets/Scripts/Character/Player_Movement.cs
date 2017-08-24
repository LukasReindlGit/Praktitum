using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    [SerializeField]
    float speed = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*Time.fixedDeltaTime*speed);
	}
}
