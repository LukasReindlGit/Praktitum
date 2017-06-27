using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{

    public enum PrimitiveTypes
    {
        cuboid,
        circle
    };

    public PrimitiveTypes primitiveType;
    public bool critical = false;
    public float shootableAngle = 90;

    private TargetPointManager manager;
    private TargetPoint[] nearestTargetPoints;

    public void Start()
    {
        /*var a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        a.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        for (int i = 0; i < 30; i++)
            Instantiate(a, getRandomHitPointOnSurface(), Quaternion.identity);*/
    }

    public void setTargetPointManager(TargetPointManager manager)
    {
        this.manager = manager;
        calculateNearestTargetPoints();
    }

    public void calculateNearestTargetPoints()
    {
        List<TargetPoint> tpList = new List<TargetPoint>();
        if (manager != null)
        {
            var targetPoints = manager.getTargetPoints();
            int length = targetPoints.Length;
            float angle;
            for (int i = 0; i < length; i++)
            {
                angle = (float) Math.Round(Vector3.Angle(gameObject.transform.forward, targetPoints[i].gameObject.transform.forward), 3);
                if (targetPoints[i] != this &&
                    (
                     targetPoints[i].shootableAngle == 180 ||
                     angle <= targetPoints[i].shootableAngle ||
                     angle <= shootableAngle)
                    )
                {
                    tpList.Add(targetPoints[i]);
                }
            }
            float aDist, bDist;
            tpList.Sort(delegate (TargetPoint a, TargetPoint b)
            {
                aDist = (a.gameObject.transform.position - gameObject.transform.position).sqrMagnitude;
                bDist = (b.gameObject.transform.position - gameObject.transform.position).sqrMagnitude;
                if (aDist > bDist)
                {
                    return 1;
                }
                else if (aDist < bDist)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
        }

        nearestTargetPoints = tpList.ToArray();
    }

    public TargetPointManager getTargetPointManager()
    {
        return manager;
    }

    public Target getCalculatedHitPoint(float accuracy, float precision)
    {
        return new Target(this, getRandomHitPointOnSurface(precision) - gameObject.transform.position);
    }

    public Vector3 getRandomHitPointOnSurface(float precision = 1.0f)
    {
        switch (primitiveType)
        {
            case PrimitiveTypes.cuboid:
                return transform.TransformPoint((UnityEngine.Random.value - 0.5f) * precision, (UnityEngine.Random.value - 0.5f) * precision, 0);
            case PrimitiveTypes.circle:
                Vector2 random = UnityEngine.Random.insideUnitCircle;
                return transform.TransformPoint(random * precision);
            default:
                return transform.position;
        }
    }

    public bool isInShootingAngle(Vector3 posWeapon)
    {
        return (Vector3.Angle(transform.position - posWeapon, transform.forward) <= shootableAngle);
    }
}
