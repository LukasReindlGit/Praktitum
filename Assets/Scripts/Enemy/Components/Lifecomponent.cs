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
    public GameObject target;

    [SerializeField]
    float maxRange;

    [SerializeField]
    float minRange;

    public GameObject explosion;
    float updatedHitPoints;

    void Awake()
    {
        if (GameHandler.players.Count > 0)
        {
            target = GameHandler.players[0];
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        updatedHitPoints = hitPoints;
    }

    public void doDamage(String tag)
    {
        var damage = 1.0f;
        switch (tag)
        {
            case "Projectile":
                damage = 1;
                break;
            case "Rocket":
                damage = 3;
                break;
        }
        updatedHitPoints -= damage;
        if(updatedHitPoints <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            destruct();
        }
    }

    public void destruct()
    {
        updatedHitPoints = hitPoints;
        SpawningSystem.instance.respawn(gameObject);
    }

    public void doDamage(float damage)
    {
        updatedHitPoints -= damage;
        if (updatedHitPoints <= 0)
        {
            destruct();
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
}
