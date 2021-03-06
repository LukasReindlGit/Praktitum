﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uses an OverlapSphere and viewing angles to get all enemies in sight of the weapon or player.
/// </summary>
public class NearestEnemySpherical
{

    private LayerMask enemyMask, rayMask;
    private Vector3 currentPosition, currentDirection;
    private float sphereRangeToCenter, sphereRadius, playersViewAngle, playersNearViewAngle, immediateProximity;

    private GameObject[] sortedEnemies;
    private List<GameObject> result;
    private Collider[] enemies;

    private bool nearEnemyInView = false, distantEnemyInView = false;
    private float maxDistance, sqrImmediateProximity, angleToEnemy, distanceToA, distanceToB;
    private int enemiesLength;
    private Vector3 rayDirection, pos, distPos, distPosRightV3, distPosLeftV3;
    private Vector2 distPosRightV2, distPosLeftV2;
    private RaycastHit hit;


    /// <summary>
    /// Constructor. Calls the initialization method of the class.
    /// </summary>
    /// <param name="enemyLayer">All layers of targetable enemies (e.g. "Shootable").</param>
    /// <param name="rayMask">All layers a raycast could hit. They are needed to check, whether an enemy was hit or not. Obstacles and walls could be possible hideouts for enemies and, therefore, should also be in this mask.</param>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction the weapon or player is looking.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    /// <param name="playersViewAngle">The maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    public NearestEnemySpherical(LayerMask enemyLayers, LayerMask rayMask, Vector3 position, Vector3 direction, float maxDistance, float playersViewAngle = 4.0f)
    {
        init(enemyLayers, rayMask, position, direction, maxDistance, playersViewAngle, 0.0f, maxDistance / 3.0f);
    }

    /// <summary>
    /// Constructor. Calls the initialization method of the class.
    /// </summary>
    /// <param name="enemyLayer">All layers of targetable enemies (e.g. "Shootable").</param>
    /// <param name="rayMask">All layers a raycast could hit. They are needed to check, whether an enemy was hit or not. Obstacles and walls could be possible hideouts for enemies and, therefore, should also be in this mask.</param>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction the weapon or player is looking.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    /// <param name="playersViewAngle">The maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    /// <param name="playersNearViewAngle">In the position's immediate proximity: the maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    public NearestEnemySpherical(LayerMask enemyLayers, LayerMask rayMask, Vector3 position, Vector3 direction, float maxDistance, float playersViewAngle, float playersNearViewAngle = 0.0f)
    {
        init(enemyLayers, rayMask, position, direction, maxDistance, playersViewAngle, playersNearViewAngle, maxDistance / 3.0f);
    }

    /// <summary>
    /// Constructor. Calls the initialization method of the class.
    /// </summary>
    /// <param name="enemyLayer">All layers of targetable enemies (e.g. "Shootable").</param>
    /// <param name="rayMask">All layers a raycast could hit. They are needed to check, whether an enemy was hit or not. Obstacles and walls could be possible hideouts for enemies and, therefore, should also be in this mask.</param>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction the weapon or player is looking.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    /// <param name="playersViewAngle">The maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    /// <param name="playersNearViewAngle">In the position's immediate proximity: the maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    /// <param name="immediateProximity">The maximum possible range the player can see with a bigger angle.</param>
    public NearestEnemySpherical(LayerMask enemyLayers, LayerMask rayMask, Vector3 position, Vector3 direction, float maxDistance, float playersViewAngle, float playersNearViewAngle, float immediateProximity)
    {
        init(enemyLayers, rayMask, position, direction, maxDistance, playersViewAngle, playersNearViewAngle, immediateProximity);
    }

    /// <summary>
    /// Initializes all the variables.
    /// </summary>
    /// <param name="enemyLayer">All layers of targetable enemies (e.g. "Shootable").</param>
    /// <param name="rayMask">All layers a raycast could hit. They are needed to check, whether an enemy was hit or not. Obstacles and walls could be possible hideouts for enemies and, therefore, should also be in this mask.</param>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction the weapon or player is looking.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    /// <param name="playersViewAngle">The maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    /// <param name="playersNearViewAngle">In the position's immediate proximity: the maximum angle the enemy position can differ from the viewing direction to be able to see it.</param>
    /// <param name="immediateProximity">The maximum possible range the player can see with a bigger angle.</param>
    private void init(LayerMask enemyLayers, LayerMask rayMask, Vector3 position, Vector3 direction, float maxDistance, float playersViewAngle, float playersNearViewAngle, float immediateProximity)
    {
        enemyMask = enemyLayers;
        this.rayMask = rayMask;
        currentDirection = direction;
        currentPosition = position;
        this.playersViewAngle = playersViewAngle;
        this.playersNearViewAngle = playersNearViewAngle;
        updateSphereParameters(maxDistance);

        this.immediateProximity = immediateProximity;
        sqrImmediateProximity = immediateProximity * immediateProximity;

        result = new List<GameObject>();
        GameHandler.OnGizmoDrawEvent += DrawGizmos; // To draw the gizmos we have to add our method to a monobehaviour event system
    }

    /// <summary>
    /// Updates all sphere parameters (radius and range to the center of the sphere). The values depend on the view angles.
    /// </summary>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    private void updateSphereParameters(float maxDistance)
    {
        this.maxDistance = maxDistance;

        if (playersNearViewAngle < 90.0f || playersViewAngle < 90.0f)
        {
            sphereRangeToCenter = this.maxDistance / 2.0f;
            sphereRadius = sphereRangeToCenter;
        }
        else
        {
            sphereRangeToCenter = 0;
            sphereRadius = this.maxDistance;
        }
    }

    /// <summary>
    /// Generates a new ordered array filled with all seen enemies from nearest to farthest.
    /// </summary>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction of the weapon or player.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    public void updateNearestEnemies(Vector3 position, Vector3 direction, float maxDistance)
    {
        updateSphereParameters(maxDistance);
        updateNearestEnemies(position, direction);
    }

    /// <summary>
    /// Generates a new ordered array filled with all seen enemies from nearest to farthest.
    /// </summary>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction of the weapon or player.</param>
    public void updateNearestEnemies(Vector3 position, Vector3 direction)
    {
        currentPosition = position;
        currentDirection = direction;

        // Search for all enemies in a sphere around the weapon
        enemies = Physics.OverlapSphere(position + new Vector3(direction.x, 0, direction.z) * sphereRangeToCenter, sphereRadius, enemyMask);
        enemiesLength = (enemies != null) ? enemies.Length : 0;

        result.Clear();

        pos = new Vector3(position.x, 0, position.z);
        distPos = pos + new Vector3(direction.x, 0, direction.z) * maxDistance;
        distPosRightV3 = Quaternion.Euler(0, playersViewAngle, 0) * (distPos - pos) + pos;
        distPosLeftV3 = Quaternion.Euler(0, -playersViewAngle, 0) * (distPos - pos) + pos;
        distPosRightV2 = new Vector2(distPosRightV3.x, distPosRightV3.z);
        distPosLeftV2 = new Vector2(distPosLeftV3.x, distPosLeftV3.z);

        for (int i = 0; i < enemiesLength; i++)
        {
            // Get all enemies in Sight
            if (enemyInSight(enemies[i].gameObject, position, direction, distPosRightV2, distPosLeftV2))
            {
                result.Add(enemies[i].gameObject);
            }
        }

        // Orders enemies from nearest to farthest
        result.Sort(delegate (GameObject a, GameObject b)
        {
            distanceToA = (a.transform.position - position).sqrMagnitude;
            distanceToB = (b.transform.position - position).sqrMagnitude;
            if (distanceToA > distanceToB)
            {
                return 1;
            }
            else if (distanceToA < distanceToB)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });

        sortedEnemies = result.ToArray();
    }

    /// <summary>
    /// Returns a new ordered array filled with all seen enemies from nearest to farthest.
    /// </summary>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction of the weapon or player.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    /// <returns>A new ordered array filled with all seen enemies from nearest to farthest.</returns>
    public GameObject[] getNearestEnemies(Vector3 position, Vector3 direction, float maxDistance)
    {
        updateNearestEnemies(position, direction, maxDistance);
        return getNearestEnemies();
    }

    /// <summary>
    /// Returns a new ordered array filled with all seen enemies from nearest to farthest.
    /// </summary>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction of the weapon or player.</param>
    /// <returns>A new ordered array filled with all seen enemies from nearest to farthest.</returns>
    public GameObject[] getNearestEnemies(Vector3 position, Vector3 direction)
    {
        updateNearestEnemies(position, direction);
        return getNearestEnemies();
    }

    /// <summary>
    /// Returns the ordered array filled with all seen enemies from nearest to farthest.
    /// </summary>
    /// <returns>The ordered array filled with all seen enemies from nearest to farthest.</returns>
    public GameObject[] getNearestEnemies()
    {
        return sortedEnemies;
    }

    /// <summary>
    /// Updates and returns the current target enemy.
    /// </summary>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction of the weapon or player.</param>
    /// <param name="maxDistance">The maximum possible range from the own position to a possible target.</param>
    /// <returns>The updated current target enemy.</returns>
    public GameObject getTargetEnemy(Vector3 position, Vector3 direction, float maxDistance)
    {
        getNearestEnemies(position, direction, maxDistance);
        return getTargetEnemy();
    }

    /// <summary>
    /// Updates and returns the current target enemy.
    /// </summary>
    /// <param name="position">The current position of the weapon or player.</param>
    /// <param name="direction">The current direction of the weapon or player.</param>
    /// <returns>The updated current target enemy.</returns>
    public GameObject getTargetEnemy(Vector3 position, Vector3 direction)
    {
        getNearestEnemies(position, direction);
        return getTargetEnemy();
    }

    /// <summary>
    /// Returns the current target enemy.
    /// </summary>
    /// <returns>The current target enemy in the sorted array of all seen enemies.</returns>
    public GameObject getTargetEnemy()
    {
        return (sortedEnemies != null && sortedEnemies.Length > 0) ? sortedEnemies[0] : null;
    }

    /// <summary>
    /// Tests, whether an enemy is in sight or not.
    /// </summary>
    /// <param name="enemy">The enemy you want to check.</param>
    /// <param name="ownPosition">The own position of the weapon or player.</param>
    /// <param name="ownDirection">The own viewing direction of the weapon or player.</param>
    /// <param name="distPosRightV2">Distant Position on right view angle line.</param>
    /// <param name="distPosLeftV2">Distant Position on left view angle line.</param>
    /// <returns>True or false, whether the enemy is in sight or not.</returns>
    private bool enemyInSight(GameObject enemy, Vector3 ownPosition, Vector3 ownDirection, Vector2 distPosRightV2, Vector2 distPosLeftV2)
    {
        rayDirection = enemy.transform.position - ownPosition;
        angleToEnemy = Vector2.Angle(new Vector2(rayDirection.x, rayDirection.z), new Vector2(ownDirection.x, ownDirection.z));

        nearEnemyInView = angleToEnemy <= playersNearViewAngle && (ownPosition - enemy.transform.position).sqrMagnitude <= sqrImmediateProximity;
        distantEnemyInView = angleToEnemy <= playersViewAngle;

        if ((nearEnemyInView || distantEnemyInView || enemyIntersectsViewAngle(enemy, ownPosition, distPosRightV2, distPosLeftV2)) && Physics.Raycast(ownPosition, rayDirection, out hit, maxDistance, rayMask))
        {
            if (hit.collider.gameObject == enemy)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Tests, whether the enemy intersects the two ViewAngle lines.
    /// </summary>
    /// <param name="enemy">The enemy to test with.</param>
    /// <param name="ownPosition">The own position of the weapon or player.</param>
    /// <param name="distPosRightV2">Distant Position on right view angle line.</param>
    /// <param name="distPosLeftV2">Distant Position on left view angle line.</param>
    /// <returns>True, if enemy intersects one of the lines.</returns>
    private bool enemyIntersectsViewAngle(GameObject enemy, Vector3 ownPosition, Vector2 distPosRightV2, Vector2 distPosLeftV2)
    {
        Bounds b = enemy.GetComponent<Collider>().bounds;
        var p = new Vector3(b.center.x, 0, b.center.z);

        var p1 = new Vector2(p.x + b.extents.x, p.z + b.extents.z);
        var p2 = new Vector2(p.x - b.extents.x, p.z - b.extents.z);
        var p3 = new Vector2(p.x + b.extents.x, p.z - b.extents.z);
        var p4 = new Vector2(p.x - b.extents.x, p.z + b.extents.z);
        var q1 = new Vector2(ownPosition.x, ownPosition.z);

        return lineSegementsIntersect(p1, p2, q1, distPosRightV2) ||
               lineSegementsIntersect(p1, p2, q1, distPosLeftV2) ||
               lineSegementsIntersect(p3, p4, q1, distPosRightV2) ||
               lineSegementsIntersect(p3, p4, q1, distPosLeftV2);
    }

    /// <summary>
    /// Tests, whether two line segments intersect.
    /// <see cref="https://stackoverflow.com/a/14795484"/>
    /// </summary>
    /// <param name="p1">Vector to the start point of p.</param>
    /// <param name="p2">Vector to the end point of p.</param>
    /// <param name="q1">Vector to the start point of q.</param>
    /// <param name="q2">Vector to the end point of q.</param>
    /// <returns>True, if an intersection was found.</returns>
    public static bool lineSegementsIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
    {
        var s10 = p2 - p1;
        var s32 = q2 - q1;

        var denom = s10.x * s32.y - s32.x * s10.y;
        if (denom == 0)
        {
            return false;
        }
        var denomPositive = denom > 0;

        var s02 = p1 - q1;
        var s_numer = s10.x * s02.y - s10.y * s02.x;
        if ((s_numer < 0) == denomPositive)
        {
            return false;
        }


        var t_numer = s32.x * s02.y - s32.y * s02.x;
        if ((t_numer < 0) == denomPositive)
        {
            return false;
        }


        if ((s_numer > denom) == denomPositive || (t_numer > denom) == denomPositive)
        {
            return false;
        }

        // Intersection point
        // var t = t_numer / denom;
        // var intersection = new Vector2(p1.x + (t * s10.x), p1.y + (t * s10.y));

        return true;
    }

    /// <summary>
    /// Draws the WireSphere (red), viewing angles (cyan and green), and marks the seen enemies (red cubes) as well as the current main target (green cube).
    /// </summary>
    private void DrawGizmos()
    {
        // Sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentPosition + currentDirection * sphereRangeToCenter, sphereRadius);

        // Local Z-Axis (View-Axis)
        Gizmos.color = Color.blue;
        Vector3 distPos = currentPosition + currentDirection * maxDistance;
        Gizmos.DrawLine(currentPosition, distPos);

        // Full View Angle
        Vector3 distPosRight = Quaternion.Euler(0, playersViewAngle, 0) * (distPos - currentPosition) + currentPosition;
        Vector3 distPosLeft = Quaternion.Euler(0, -playersViewAngle, 0) * (distPos - currentPosition) + currentPosition;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(currentPosition, distPosRight);
        Gizmos.DrawLine(currentPosition, distPosLeft);
        Gizmos.DrawLine(distPosLeft, distPosRight);

        // Proximity View Angle
        Vector3 distPosNear = currentPosition + currentDirection * immediateProximity;
        Vector3 distPosNearRight = Quaternion.Euler(0, playersNearViewAngle, 0) * (distPosNear - currentPosition) + currentPosition;
        Vector3 distPosNearLeft = Quaternion.Euler(0, -playersNearViewAngle, 0) * (distPosNear - currentPosition) + currentPosition;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(currentPosition, distPosNearRight);
        Gizmos.DrawLine(currentPosition, distPosNearLeft);
        Gizmos.DrawLine(distPosNearLeft, distPosNearRight);

        // Mark enemies (green = target, red = other seen enemies)
        int length = (sortedEnemies != null) ? sortedEnemies.Length : 0;
        for (int i = 0; i < length; i++)
        {
            if (i != 0)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawWireCube(sortedEnemies[i].transform.position, sortedEnemies[i].GetComponent<Renderer>().bounds.size);
        }
    }
}
