using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    float healthPoints = 10;

    public void doDamage(float damage)
    {
        healthPoints -= damage;
        Debug.Log(healthPoints + " Healthpoints Remaining!");
        if(healthPoints <= 0)
        {
            Debug.Log("You are DEAD!");
        }
    }
}
