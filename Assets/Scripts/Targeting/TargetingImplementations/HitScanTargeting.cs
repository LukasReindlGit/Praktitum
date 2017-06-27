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
        List<TargetPoint> copiedPoints = new List<TargetPoint>();
        copiedPoints.AddRange(points);
        int copiedPointsLength, rnd;
        TargetPoint tarPoint;

        // Check if critical hit
        if (UnityEngine.Random.value <= parameters.CriticalChance)
        {
            /*lengthCopiedPoints = crits;
            copiedPoints = new TargetPoint[lengthCopiedPoints];
            Array.Copy(points, copiedPoints, lengthCopiedPoints);*/
            copiedPoints.RemoveRange(crits, copiedPoints.Count - crits);

        }
        else
        {
            /*lengthCopiedPoints = points.Length - crits;
            copiedPoints = new TargetPoint[lengthCopiedPoints];
            Array.Copy(points, crits, copiedPoints, 0, lengthCopiedPoints);*/
            copiedPoints.RemoveRange(0, crits);
        }

        copiedPointsLength = copiedPoints.Count;
        while (copiedPointsLength > 0)
        {
            rnd = UnityEngine.Random.Range(0, copiedPointsLength);
            tarPoint = copiedPoints[rnd];
            if (tarPoint.isInShootingAngle(position))
            {
                targets[0] = new Target(tarPoint, tarPoint.getRandomHitPointOnSurface(parameters.Accuracy) - tarPoint.gameObject.transform.position);
                break;
            }
            copiedPoints.RemoveAt(rnd);
            copiedPointsLength -= 1;
        }
        if (copiedPointsLength == 0)
        {
            for (int i = 0; i < length; i++)
            {
                targets[i] = new Target(points[0], points[0].getRandomHitPointOnSurface(parameters.Accuracy) - points[0].gameObject.transform.position);
            }
        }


        return targets;
    }

    public override void UpdateTargetSystem(Vector3 position, Vector3 direction)
    {
        system.updateNearestEnemies(position, direction);
    }
}
