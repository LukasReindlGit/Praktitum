using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.Component;
using System;

public class Lifecomponent : MonoBehaviour {

    //ehemals nur fürs leben, dient es nun als variablen-speicherklasse auf die die anderen module zugreifen können
    [SerializeField]
    float hitPoints;

    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    float maxRange;

    [SerializeField]
    float minRange;

    

    public void doDamage(float damage)
    {
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public float MaxRange
    {
        get
        {
            return maxRange;
        }

        set
        {
            maxRange = value;
        }
    }

    public float MinRange
    {
        get
        {
            return minRange;
        }

        set
        {
            minRange = value;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Test");
        if (other.gameObject.CompareTag("Projectile"))
        {
            doDamage(1);
            Debug.Log("HIT");
        }
        else if (other.gameObject.CompareTag("Rocket"))
        {
            doDamage(3);
        }
    }
}
