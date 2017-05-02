using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public GameObject projectile;
    public float distance;
    public int clipsize;
    public float frequency;
    public Vector3[] spawnpoints;

    public virtual void OnEnable()
    {
        usedTargetingSystem = new TestTargetingSystem();
    }

    public override Weapon Spawn(Player player)
    {
        return this;
    }
        
    public override void TryShoot(Vector3 direction)
    {        
        throw new NotImplementedException();
    }
    
}
