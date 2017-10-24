using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningSystem : MonoBehaviour {

    public static SpawningSystem instance;

    [SerializeField]
    int MaxEnemyAmount = 10;

    [SerializeField]
    GameObject[] enemyPrefabs;

    [SerializeField]
    GameObject[] spawnLocations;

    private List<GameObject> EnemyObjects;
    private GameObject enemyParent;

	// Use this for initialization
	void Awake () {
        instance = this;

        EnemyObjects = new List<GameObject>();
        enemyParent = new GameObject("Enemies");

        if (enemyPrefabs != null && enemyPrefabs.Length > 0) {
            int randEnemy, randPos;
            for (int i = 0; i < MaxEnemyAmount; i++)
            {
                randEnemy = UnityEngine.Random.Range(0, enemyPrefabs.Length);
                randPos = UnityEngine.Random.Range(0, spawnLocations.Length);
                var enemy = GameObject.Instantiate(enemyPrefabs[randEnemy], spawnLocations[randPos].transform.position, Quaternion.identity, enemyParent.transform);
                EnemyObjects.Add(enemy);
            }
        }
        
	}

    public void respawn(GameObject enemy)
    {
        GameObject.Destroy(enemy);
        //enemy.transform.position = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Length)].transform.position;
        StartCoroutine(setActiveAfterCooldown(enemy, UnityEngine.Random.Range(1.0f, 5.0f)));
    }
	
	IEnumerator setActiveAfterCooldown(GameObject obj, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        EnemyObjects.Add(GameObject.Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity, enemyParent.transform));
    }
}
