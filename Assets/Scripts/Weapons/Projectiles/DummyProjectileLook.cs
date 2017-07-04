using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyProjectileLook : MonoBehaviour {

    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float destroyAfter = 1;

    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rigid.velocity = transform.forward * projectileSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log("DESTROYING: " + other.tag+ "   "+other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
