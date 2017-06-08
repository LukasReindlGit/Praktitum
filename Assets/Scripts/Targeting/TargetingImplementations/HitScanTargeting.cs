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
                                            this.weapon.transform.position, 
                                            this.weapon.transform.forward,
                                            this.weapon.param.Range,
                                            this.weapon.param.Angle
                                          );
    }

    public override Target[] GetTargets(Vector3 position, Vector3 direction, Parameters parameters)
    {
        Target[] targets = new Target[(parameters.SalveCount > 0) ? parameters.SalveCount : 0];
        int length = targets.Length;

        target = system.getTargetEnemy();
        if (target == null)
        {
            return null;
        }

        TargetPointManager targetPointManager = target.GetComponentInChildren<TargetPointManager>();
        TargetPoint[] points = targetPointManager.getTargetPoints();
        int crits = targetPointManager.getCriticalCount();
        List<TargetPoint> copiedPoints = new List<TargetPoint>(points);
        int lengthCopiedPoints;
        
        // Check if critical hit
        if (UnityEngine.Random.value <= parameters.CriticalChance)
        {
            /*lengthCopiedPoints = crits;
            copiedPoints = new TargetPoint[lengthCopiedPoints];
            Array.Copy(points, copiedPoints, lengthCopiedPoints);*/
            copiedPoints.RemoveRange(crits, copiedPoints.Count - crits);
            while (copiedPoints.Count > 0)
            {
                int rnd = UnityEngine.Random.Range(0, copiedPoints.Count);
                TargetPoint p = copiedPoints[rnd];
                if (p.isInShootingAngle(position))
                {
                    targets[0] = new Target(p, p.getRandomHitPointOnSurface() - p.gameObject.transform.position);
                    break;
                }
                copiedPoints.RemoveAt(rnd);
            }
        }
        else
        {
            /*lengthCopiedPoints = points.Length - crits;
            copiedPoints = new TargetPoint[lengthCopiedPoints];
            Array.Copy(points, crits, copiedPoints, 0, lengthCopiedPoints);*/
            copiedPoints.RemoveRange(0, crits);
            while (copiedPoints.Count > 0)
            {

            }
        }

        return targets;
    }

    public override void UpdateTargetSystem(Vector3 position, Vector3 direction)
    {
        system.updateNearestEnemies(position, direction);
    }
}
