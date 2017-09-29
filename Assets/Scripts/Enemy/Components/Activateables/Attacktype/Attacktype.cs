using AI.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Attacktype : MonoBehaviour, IActivateable
{
    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected float maxRange; //maximum range an enemy can attack

    [SerializeField]
    [Range(0, 1)]
    protected float StoppingRange = 0.1f; //range - (range/stopping range) is the minimum range an enemy can attack

    [SerializeField]
    protected MonoBehaviour activateable;

    [SerializeField]
    protected float damage = 1;

    [SerializeField]
    protected float cooldown = 2;

    protected bool active = false;
    protected bool check = true;
    protected float minRange;
    protected Lifecomponent life;

    public void Start()
    {
        minRange = maxRange - (maxRange * StoppingRange);
        GetComponentInParent<Lifecomponent>().MaxRange = maxRange;
        GetComponentInParent<Lifecomponent>().MinRange = minRange;
        life = GetComponentInParent<Lifecomponent>();
    }

    public void Update()
    {
        if (active && check)
        {
            Attack();
            //Cooldown
            StartCoroutine(CoolDown(cooldown));
        }
    }

    public void Activate(ActivateableState state = ActivateableState.NONE)
    {
        active = !active;
    }

    public void Attack()
    {
        
        target = life.GetTarget();
        target.GetComponent<PlayerHealth>().doDamage(damage);
        Instantiate(life.explosion, transform.position, Quaternion.identity);
        destruct();

        /*if (Vector3.Distance(transform.position, target.transform.position) > maxRange)
        {
            active = !active;

            if (activateable != null)
            {
                (activateable as IActivateable).Activate();
                GetComponentInParent<Lifecomponent>().SetTarget(target);
            }

        }*/
    }

    public void destruct()
    {
        life.destruct();
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        GetComponentInParent<Lifecomponent>().SetTarget(newTarget);
    }

    IEnumerator CoolDown(float coolDown)
    {
        check = false;
        yield return new WaitForSeconds(coolDown);
        check = true;

    }

}
