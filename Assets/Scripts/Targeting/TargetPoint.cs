using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour {

    public enum PrimitiveTypes
    {
        cuboid,
        circle
    };

    public PrimitiveTypes primitiveType;
    public bool critical = false;
    public float shootableAngle = 90;

	private TargetPointManager manager;
    private Vector3 shootDirection;
    private float angleOfShootDirection;

    public void setTargetPointManager(TargetPointManager manager)
    {
        this.manager = manager;
    }

    public TargetPointManager getTargetPointManager()
    {
        return manager;
    }

    public Vector3 getRandomHitPointOnSurface()
    {
        switch (primitiveType)
        {
            case PrimitiveTypes.cuboid:
                return transform.TransformPoint(Random.value - 0.5f, Random.value - 0.5f, 0);
            case PrimitiveTypes.circle:
                Vector2 random = Random.insideUnitCircle;
                return transform.TransformPoint(random.x, random.y, 0);
            default:
                return transform.position;
        }
    }

    public bool isInShootingAngle(Vector3 posWeapon)
    {
        shootDirection = transform.position - posWeapon;
        angleOfShootDirection = Vector3.Angle(shootDirection, transform.forward);
        return (angleOfShootDirection <= shootableAngle);
    }
}
