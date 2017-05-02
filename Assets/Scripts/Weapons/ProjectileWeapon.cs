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

    public override void Spawn()
    {
        throw new NotImplementedException();
    }

    public override void TryShoot()
    {
        throw new NotImplementedException();
    }
}
