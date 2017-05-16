using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestEnemySpherical : MonoBehaviour
{

    
    public LayerMask enemyMask, rayMask;
    public float playersViewAngle = 10f;
    public float playersNearViewAngle = 15f;
    public float immediateProximity = 15f;
    
    private GameObject[] sortedEnemies;

    private List<GameObject> result;
    private Collider[] enemies;
    private float range;
    private bool nearEnemyInView = false;
    private bool distantEnemyInView = false;
    private float distanceToEnemy;
    private float angleToEnemy;

    // Use this for initialization
    void Start()
    {
        enemyMask = LayerMask.GetMask("Shootable");
        rayMask = LayerMask.GetMask("Shootable", "Obstacle");
        result = new List<GameObject>();
        GameHandler.OnGizmoDrawEvent += DrawGizmos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        GameObject[] list = getNearestEnemies(transform.position, transform.forward, 20);
        
    }

    public void updateNearestEnemies(Vector3 position, Vector3 direction, float maxDistance)
    {
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
        Gizmos.DrawWireSphere(transform.position + transform.forward.normalized * range, range);

        // Local Z-Axis (View-Axis)
        Gizmos.color = Color.blue;
        Vector3 distPos = transform.position + transform.forward.normalized * range * 2;
        Gizmos.DrawLine(transform.position, distPos);

        // Full View Angle
        Vector3 distPosRight = Quaternion.Euler(0, playersViewAngle, 0) * (distPos - transform.position) + transform.position;
        Vector3 distPosLeft = Quaternion.Euler(0, -playersViewAngle, 0) * (distPos - transform.position) + transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, distPosRight);
        Gizmos.DrawLine(transform.position, distPosLeft);
        Gizmos.DrawLine(distPosLeft, distPosRight);

        // Proximity View Angle
        Vector3 distPosNear = transform.position + transform.forward.normalized * immediateProximity;
        Vector3 distPosNearRight = Quaternion.Euler(0, playersNearViewAngle, 0) * (distPosNear - transform.position) + transform.position;
        Vector3 distPosNearLeft = Quaternion.Euler(0, -playersNearViewAngle, 0) * (distPosNear - transform.position) + transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, distPosNearRight);
        Gizmos.DrawLine(transform.position, distPosNearLeft);
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
