using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class HitScanTargeting : TargetingSystem
{
    private NearestEnemySpherical system;
    private WeaponBehaviour weapon;

    public HitScanTargeting(WeaponBehaviour weapon)
    {
        this.weapon = weapon;
        system = new NearestEnemySpherical( 
                                            LayerMask.GetMask("Shootable"), 
                                            LayerMask.GetMask("Shootable", "Obstacle"), 
                                            weapon.transform.position, 
                                            weapon.transform.forward,
                                            weapon.param.Range,
                                            weapon.param.Angle
                                          );
    }

    public override Target[] GetTargets(Vector3 direction, Parameters parameters)
    {
        
        throw new NotImplementedException();
    }

    public override void UpdateTargetSystem(Vector3 position, Vector3 direction)
    {
        throw new NotImplementedException();
    }
}
