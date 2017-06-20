using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointManager : MonoBehaviour {

    /// <summary>
    /// Array of all target points sorted by first critical elements and then uncritical elements
    /// </summary>
    private TargetPoint[] targets;

    /// <summary>
    /// Amount of critical target points (Or: Position of the first uncritical target point in the array)
    /// </summary>
    private int criticalCount;

	// Use this for initialization
	void Start () {
        // Get all target points on the enemy
        targets = transform.parent.gameObject.GetComponentsInChildren<TargetPoint>();

        // Sort the array: First critical target points, then uncritical target points
        Array.Sort(targets, delegate (TargetPoint a, TargetPoint b) {
            if (a.critical && !b.critical)
            {
                return -1;
            }
            else if (!a.critical && b.critical)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });

        // Assign this manager script to all target points and initialize critical count
        int targetsLength = targets.Length;
        for (int i = 0; i < targetsLength; i++)
        {
            targets[i].setTargetPointManager(this);

            if (targets[i].critical)
            {
                criticalCount += 1;
            }
        }
	}
	
    /// <summary>
    /// Get an array with all target points on the enemy. It is sorted by critical target points first and then uncritical target points.
    /// </summary>
    /// <returns>sorted TargetPoint array (first critical elements then uncritical elements)</returns>
	public TargetPoint[] getTargetPoints()
    {
        return targets;
    }

    /// <summary>
    /// Amount of critical target points (Or: Position of the first uncritical target point in the array)
    /// </summary>
    /// <returns>Amount of critical target points</returns>
    public int getCriticalCount()
    {
        return criticalCount;
    }
}
