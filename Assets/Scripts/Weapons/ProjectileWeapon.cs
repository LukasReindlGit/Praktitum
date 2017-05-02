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

    public void OnEnable()
    {
        usedTargetingSystem = new TestTargetingSystem();
    }

    public override void Spawn()
    {
        throw new NotImplementedException();
    }

        
    public override void TryShoot(Vector3 direction)
    {        
        throw new NotImplementedException();
    }
}
