using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target {

    public Target(GameObject targetObj, Vector3 offset)
    {
        this.targetObject = targetObj;
        this.offset = offset;
    }

    private GameObject targetObject;
    private Vector3 offset;
    public Vector3 TargetPos { get { return targetObject.transform.position + offset; } }

}
