using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour {

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    bool onTrigger = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (onTrigger)
            return;

        FindObjectOfType<CameraWeaponEffect>().CallEffect();

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onTrigger)
            return;

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
