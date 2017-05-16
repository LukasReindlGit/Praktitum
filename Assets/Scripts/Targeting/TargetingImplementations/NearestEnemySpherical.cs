using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestEnemySpherical 
{

    
    private LayerMask enemyMask, rayMask;
    private Vector3 currentPosition;
    private Vector3 currentDirection;
    private float playersViewAngle = 10f;
    private float playersNearViewAngle = 15f;
    private float immediateProximity = 15f;
    
    private GameObject[] sortedEnemies;

    private List<GameObject> result;
    private Collider[] enemies;
    private float range;
    private bool nearEnemyInView = false;
    private bool distantEnemyInView = false;
    private float distanceToEnemy;
    private float angleToEnemy;

    // Use this for initialization
    public NearestEnemySpherical(LayerMask enemyLayer, LayerMask rayMask, Vector3 position, Vector3 direction, float range, float playersViewAngle = 5, float playersNearViewAngle = 10, float immediateProximity = 10)
    {
        this.enemyMask = enemyLayer;
        this.rayMask = rayMask;
        this.currentDirection = direction.normalized;
        this.currentPosition = position;
        this.range = range;
        this.playersViewAngle = playersViewAngle;
        this.playersNearViewAngle = playersNearViewAngle;
        this.immediateProximity = immediateProximity;

        result = new List<GameObject>();
        GameHandler.OnGizmoDrawEvent += DrawGizmos;
    }

    public void updateNearestEnemies(Vector3 position, Vector3 direction, float maxDistance)
    {
        currentPosition = position;
        currentDirection = direction.normalized;

        // Search for all enemies in a sphere around the weapon
        range = maxDistance / 2.0f;
        enemies = Physics.OverlapSphere(position + new Vector3(direction.x, 0, direction.z).normalized * range, range, enemyMask);

        result.Clear();
        for (int i = 0; i < enemies.Length; i++)
        {
            // Get all enemies in Sight
            if (enemyInSight(enemies[i].gameObject, position, direction))
            {
                result.Add(enemies[i].gameObject);
            }
        }

        result.Sort((a, b) => {
            if ((a.transform.position - position).sqrMagnitude > (b.transform.position - position).sqrMagnitude)
            {
                return 1;
            }
            else if ((a.transform.position - position).sqrMagnitude < (b.transform.position - position).sqrMagnitude)
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

    public GameObject[] getNearestEnemies(Vector3 position, Vector3 direction, float maxDistance)
    {
        updateNearestEnemies(position, direction, maxDistance);
        return getNearestEnemies();
    }
    public GameObject[] getNearestEnemies()
    {
        return sortedEnemies;
    }

    public GameObject getTargetEnemy(Vector3 position, Vector3 direction, float maxDistance)
    {
        GameObject[] arrayEnemies = getNearestEnemies(position, direction, maxDistance);
        return getTargetEnemy(arrayEnemies);//getTargetEnemy(position, direction, listOfEnemies);
    }

    public GameObject getTargetEnemy(GameObject[] arrayEnemies) 
    {
        return (arrayEnemies.Length > 0) ? arrayEnemies[0] : null;
    }

    /*public GameObject getTargetEnemy(Vector3 position, Vector3 direction, List<GameObject> listOfEnemies)
    {
        GameObject result = (listOfEnemies.Count > 0) ? listOfEnemies[0] : null;
        float distance = (result != null) ? (position - result.transform.position).sqrMagnitude : 0f;
        float newDistance = 0f;
        for (int i = 1; i < listOfEnemies.Count; i++)
        {
            newDistance = (position - listOfEnemies[i].transform.position).sqrMagnitude;
            if (newDistance < distance)
            {
                distance = newDistance;
                result = listOfEnemies[i];
            }
        }

        return result;
    }*/

    private bool enemyInSight(GameObject enemy, Vector3 ownPosition, Vector3 ownDirection)
    {
        RaycastHit hit;
        Vector3 rayDirection = enemy.transform.position - ownPosition;
        distanceToEnemy = (ownPosition - enemy.transform.position).sqrMagnitude;
        angleToEnemy = Vector2.Angle(new Vector2(rayDirection.x, rayDirection.z), new Vector2(ownDirection.x, ownDirection.z));

        nearEnemyInView = angleToEnemy <= playersNearViewAngle && distanceToEnemy <= immediateProximity * immediateProximity;
        distantEnemyInView = angleToEnemy <= playersViewAngle;

        if ((nearEnemyInView || distantEnemyInView) && Physics.Raycast(ownPosition, rayDirection, out hit, range * 2, rayMask))
        {
            if (hit.collider.gameObject == enemy)
                return true;
        }

        return false;
    }

    private void DrawGizmos()
    {
        // Sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentPosition + currentDirection * range, range);

        // Local Z-Axis (View-Axis)
        Gizmos.color = Color.blue;
        Vector3 distPos = currentPosition + currentDirection * range * 2;
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
        int length = sortedEnemies.Length;
        for (int i = 0; i < length; i++)
        {
            if (i != 0)
            {
                Gizmos.color = Color.red;
            } else
            {
                Gizmos.color = Color.green;
            }
            
            Gizmos.DrawWireCube(sortedEnemies[i].transform.position, sortedEnemies[i].GetComponent<Renderer>().bounds.size);
        }
    }
}
