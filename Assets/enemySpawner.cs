using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnInterval;
    float spawnTimer;
    int wall;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            wall = Random.Range(1, 4);
            if (wall == 1)
            {
                Instantiate(enemy, new Vector3(-49, 1, Random.Range(-23f, 23f)), Quaternion.identity);
            }
            else if (wall == 2)
            {
                Instantiate(enemy, new Vector3(49, 1, Random.Range(-23f, 23f)), Quaternion.identity);
            }
            else if (wall == 3)
            {
                Instantiate(enemy, new Vector3(Random.Range(-48f, 48f), 1, 24), Quaternion.identity);
            }
            else if (wall == 4)
            {
                Instantiate(enemy, new Vector3(Random.Range(-48f, 48f), 1, -24), Quaternion.identity);
            }
            spawnTimer = spawnInterval;
        }
    }
}
