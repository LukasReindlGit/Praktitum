using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour {

    public bool critical = false;
    public float shootableDegree = 90;

	private TargetPointManager manager;

    public void setTargetPointManager(TargetPointManager m)
    {
        manager = m;
    }

    public TargetPointManager getTargetPointManager()
    {
        return manager;
    }
}
