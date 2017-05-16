using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBuildings : MonoBehaviour {

    [SerializeField]
    GameObject target;

    Renderer rend;
    bool was = true;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z<target.transform.position.z)
        {
         if(was)
            {
                was = false;
                rend.enabled = false;
            }   
        }
        else
        {
            if(was==false)
            {
                rend.enabled = true;
                was = true;
            }

        }
	}
}
