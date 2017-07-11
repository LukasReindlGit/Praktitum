using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI.Component;

public class Lifecomponent : MonoBehaviour {

    [SerializeField]
    float hitPoints;

    public void doDamage(float damage)
    {
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
