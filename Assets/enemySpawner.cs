using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject enemyR;
    GameObject choice;
    public float spawnInterval;
    float spawnTimer;
    int wall;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Random.Range(-1f, 1f) > 0)
            {
                choice = enemy;
            }
            else
            {
                choice = enemyR;
            }
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                wall = Random.Range(1, 4);
                if (wall == 1)
                {
                    Instantiate(choice, new Vector3(-49, 1, Random.Range(-23f, 23f)), Quaternion.identity);
                }
                else if (wall == 2)
                {
                    Instantiate(choice, new Vector3(49, 1, Random.Range(-23f, 23f)), Quaternion.identity);
                }
                else if (wall == 3)
                {
                    Instantiate(choice, new Vector3(Random.Range(-48f, 48f), 1, 24), Quaternion.identity);
                }
                else if (wall == 4)
                {
                    Instantiate(choice, new Vector3(Random.Range(-48f, 48f), 1, -24), Quaternion.identity);
                }
                spawnTimer = spawnInterval;
            }
        }
    }
}
