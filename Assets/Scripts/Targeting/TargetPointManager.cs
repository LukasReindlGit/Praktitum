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
    /// Array of all critical target points
    /// </summary>
    private TargetPoint[] critTargets;

    /// <summary>
    /// Array of all uncritical target points
    /// </summary>
    private TargetPoint[] uncritTargets;

    /// <summary>
    /// Amount of critical target points (Or: Position of the first uncritical target point in the targets array)
    /// </summary>
    private int criticalCount;

    /// <summary>
    /// Amount of uncritical target points
    /// </summary>
    private int uncriticalCount;

	// Use this for initialization
	void Start () {
        // Get all target points on the enemy
        targets = transform.parent.gameObject.GetComponentsInChildren<TargetPoint>();

        if (targets == null || targets.Length == 0)
        {
            throw new MissingComponentException("Target does not have any TargetPoints!");
        }

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

        uncriticalCount = targetsLength - criticalCount;
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
    /// Get an array with all critical target points on the enemy.
    /// </summary>
    /// <returns>critical TargetPoint array</returns>
    public TargetPoint[] getCriticalTargetPoints()
    {
        if (critTargets == null) {
            critTargets = new TargetPoint[criticalCount];
            Array.Copy(targets, critTargets, criticalCount);
        }
        return critTargets;
    }

    /// <summary>
    /// Get an array with all uncritical target points on the enemy.
    /// </summary>
    /// <returns>uncritical TargetPoint array</returns>
    public TargetPoint[] getUncriticalTargetPoints()
    {
        if (uncritTargets == null)
        {
            uncritTargets = new TargetPoint[targets.Length - criticalCount];
            Array.Copy(targets, criticalCount, uncritTargets, 0, targets.Length - criticalCount);
        }
        return uncritTargets;
    }

    /// <summary>
    /// Amount of critical target points (Or: Position of the first uncritical target point in the array)
    /// </summary>
    /// <returns>Amount of critical target points</returns>
    public int getCriticalCount()
    {
        return criticalCount;
    }

    /// <summary>
    /// Amount of uncritical target points
    /// </summary>
    /// <returns>Amount of uncritical target points</returns>
    public int getUncriticalCount()
    {
        return uncriticalCount;
    }
}
