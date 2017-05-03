using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestEnemySpherical : MonoBehaviour {

    private Collider[] enemies;
    public LayerMask enemyMask, rayMask;
    public float playersViewAngle = 90f;
    public float playersNearViewAngle = 110f;
    public float immediateProximity = 15f;

    private List<GameObject> result;
    private bool nearEnemyInView = false;
    private bool distantEnemyInView = false;
    private float distanceToEnemy = 0f;

	// Use this for initialization
	void Start () {
        enemyMask = LayerMask.GetMask("Shootable");
        rayMask = LayerMask.GetMask("Shootable", "Obstacle");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GameObject[] obj = FindObjectsOfType<GameObject>();

        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].layer == LayerMask.NameToLayer("Shootable"))
            {
                obj[i].GetComponent<MeshRenderer>().material.color = Color.gray;
            }
        }

        List<GameObject> list = getNearestEnemies(transform.position, transform.forward, 50);
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<MeshRenderer>().material.color = Color.red;
        }

        getTargetEnemy(transform.position, transform.forward, list).GetComponent<MeshRenderer>().material.color = Color.green;

	}

    public List<GameObject> getNearestEnemies(Vector3 position, Vector3 direction, float maxDistance)
    {
        // Search for all enemies in a sphere around the weapon
        enemies = Physics.OverlapSphere(position, maxDistance, enemyMask);

        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            // Get all enemies in Sight
            if (enemyInSight(enemies[i].gameObject, position, direction))
            {
                result.Add(enemies[i].gameObject);
            }
        }
        return result;
    }

    public GameObject getTargetEnemy(Vector3 position, Vector3 direction, float maxDistance)
    {
        List<GameObject> listOfEnemies = getNearestEnemies(position, direction, maxDistance);
        return getTargetEnemy(position, direction, listOfEnemies);
    }

    public GameObject getTargetEnemy(Vector3 position, Vector3 direction, List<GameObject> listOfEnemies)
    {
        GameObject result = (listOfEnemies.Count > 0) ? listOfEnemies[0] : null;
        float distance = (result != null) ? Vector3.Distance(position, result.transform.position) : 0f;
        float newDistance = 0f;

        for (int i = 1; i < listOfEnemies.Count; i++)
        {
            newDistance = Vector3.Distance(position, listOfEnemies[i].transform.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                result = listOfEnemies[i];
            }
        }

        return result;
    }

    private bool enemyInSight(GameObject enemy, Vector3 ownPosition, Vector3 ownDirection)
    {
        RaycastHit hit;
        Vector3 rayDirection = enemy.transform.position - ownPosition;
        distanceToEnemy = Vector3.Distance(ownPosition, enemy.transform.position);

        nearEnemyInView = Vector3.Angle(rayDirection, ownDirection) <= playersNearViewAngle
                       && distanceToEnemy <= immediateProximity;
        distantEnemyInView = Vector3.Angle(rayDirection, ownDirection) <= playersViewAngle;

        if ((nearEnemyInView || distantEnemyInView) && Physics.Raycast(ownPosition, rayDirection, out hit, distanceToEnemy * 2, rayMask))
        {
            if (hit.collider.gameObject == enemy)
                return true;
        }

        return false;
    }
}
