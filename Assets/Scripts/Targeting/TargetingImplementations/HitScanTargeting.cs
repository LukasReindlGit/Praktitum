using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class HitScanTargeting : TargetingSystem
{
    private NearestEnemySpherical system;
    private WeaponBehaviour weapon;
    private GameObject target;

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
        Target[] targets = new Target[parameters.SalveCount];
        int length = targets.Length;

        target = system.getTargetEnemy();
        if (target == null)
        {
            return null;
        }

        for (int i = 0; i < length; i++)
        {
            targets[i] = new Target(target, Vector3.zero);
        }

        return targets;
    }

    public override void UpdateTargetSystem(Vector3 position, Vector3 direction)
    {
        system.updateNearestEnemies(position, direction);
    }
}
