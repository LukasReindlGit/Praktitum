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
        //var a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //a.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //for (int i = 0; i < 50; i++)
        //    Instantiate(a, getRandomHitPointOnSurface(0.5f), Quaternion.identity);
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
        return new Target(this, getRandomHitPointOnSurface(precision) - transform.position);
    }

    public Vector3 getRandomHitPointOnSurface(float precision = 1.0f)
    {
        precision = 1.0f - precision;
        precision = (precision <= 0.01f) ? 0.01f : precision;
        switch (primitiveType)
        {
            case PrimitiveTypes.cuboid:
                return transform.TransformPoint((UnityEngine.Random.value - 0.5f) * precision, (UnityEngine.Random.value - 0.5f) * precision, 0);
            case PrimitiveTypes.circle:
                double a = UnityEngine.Random.value;
                double b = UnityEngine.Random.value;
                if (b < a) {
                    var temp = b;
                    b = a;
                    a = temp;
                }
                var newPointX = b * precision * Math.Cos(2 * Math.PI * a / b);
                var newPointY = b * precision * Math.Sin(2 * Math.PI * a / b);
                return transform.TransformPoint((float) newPointX, (float) newPointY, 0);
            default:
                return transform.position;
        }
    }

    public bool isInShootingAngle(Vector3 posWeapon)
    {
        return Vector3.Angle(posWeapon - gameObject.transform.position, gameObject.transform.forward) <= shootableAngle;
    }
}
