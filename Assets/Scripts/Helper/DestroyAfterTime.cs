using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float delay = 5;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, delay);
	}
	
}
