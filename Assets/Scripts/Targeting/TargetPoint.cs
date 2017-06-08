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

    public void Start()
    {
        var a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        a.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        for (int i = 0; i < 30; i++)
            Instantiate(a, getRandomHitPointOnSurface(), Quaternion.identity);
    }

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
                return transform.TransformPoint(Random.insideUnitSphere);
            default:
                return transform.position;
        }
    }

    public bool isInShootingAngle(Vector3 posWeapon)
    {
        return (Vector3.Angle(transform.position - posWeapon, transform.forward) <= shootableAngle);
    }
}
