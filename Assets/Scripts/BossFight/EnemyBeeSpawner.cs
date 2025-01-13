using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeSpawner : MonoBehaviour
{
    public GameObject enemyBee;
    public float spawnInterval;
    public Transform[] spawnPoints;

    bool isSpawning = false;

    void Start()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            if (spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length); //rastgele spawn noktasý seçilir
                Instantiate(enemyBee, spawnPoints[randomIndex].position, Quaternion.identity); 
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }
}
