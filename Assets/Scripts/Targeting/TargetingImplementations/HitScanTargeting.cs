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

    private bool wasCritical;
    private TargetPointManager targetPointManager;
    private int rnd, copiedPointsLength;
    private TargetPoint tarPoint;

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
        if (parameters.SalveCount <= 0)
        {
            return null;
        }
        Target[] targets = new Target[parameters.SalveCount];
        int length = targets.Length;

        target = system.getTargetEnemy();
        if (target == null)
        {
            return null;
        }

        targetPointManager = target.GetComponentInChildren<TargetPointManager>();
        if (targetPointManager == null)
        {
            throw new MissingComponentException("Target does not have any TargetPointManager!");
        }

        List<TargetPoint> copiedPoints;

        // Check if critical hit
        if (wasCritical = (UnityEngine.Random.value <= parameters.CriticalChance))
        {
            copiedPoints = new List<TargetPoint>(targetPointManager.getCriticalTargetPoints());
            copiedPointsLength = targetPointManager.getCriticalCount();
        }
        else
        {
            copiedPoints = new List<TargetPoint>(targetPointManager.getUncriticalTargetPoints());
            copiedPointsLength = targetPointManager.getUncriticalCount();
        }

        for (int i = 0; i < 1; i++)
        {
            while (copiedPointsLength > 0)
            {
                rnd = UnityEngine.Random.Range(0, copiedPointsLength);
                tarPoint = copiedPoints[rnd];
                if (tarPoint.isInShootingAngle(position))
                {
                    targets[0] = tarPoint.getCalculatedHitPoint(parameters.Accuracy, parameters.Precision);
                    break;
                }
                copiedPoints.RemoveAt(rnd);
                copiedPointsLength -= 1;
            }

            if (copiedPointsLength == 0)
            {
                if (wasCritical)
                {
                    copiedPoints = new List<TargetPoint>(targetPointManager.getUncriticalTargetPoints());
                    copiedPointsLength = targetPointManager.getUncriticalCount();
                }
                else
                {
                    copiedPoints = new List<TargetPoint>(targetPointManager.getCriticalTargetPoints());
                    copiedPointsLength = targetPointManager.getCriticalCount();
                }
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < length; i++)
        {
            targets[i] = tarPoint.getCalculatedHitPoint(parameters.Accuracy, parameters.Precision);
        }

        return targets;
    }

    public override void UpdateTargetSystem(Vector3 position, Vector3 direction)
    {
        system.updateNearestEnemies(position, direction);
    }
}
