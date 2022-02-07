using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject rEnemy;
    GameObject choice;
    GameObject world;

    float spawnTimer;
    float health = 5;

    public Material flash;
    public Material red;
    Light light;

    void Start()
    {
        world = GameObject.Find("World");
    }

    void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime;
        if (Random.Range(0f, 2f) <= 1){
            choice = enemy;
        }
        else{
            choice = rEnemy;
        }
        if (spawnTimer <= 0){
            Instantiate(choice, transform.position, transform.rotation);
            spawnTimer = 5;
        }
        if (world.GetComponent<worldController>().stage == "Boss"){
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision info){
        if (info.collider.tag == "Bullet"){
            health -= 1;
            StartCoroutine(Flash());
            if (health <= 0){
                Destroy(this.gameObject);
                world.GetComponent<worldController>().spawnerSpawned = false;
            }
            Destroy(info.collider.gameObject);
        }
    }
    IEnumerator Flash()
    {
        light.color = Color.white;
        GetComponent<MeshRenderer>().material = flash;
        yield return new WaitForSeconds(0.08f);
        light.color = Color.red;
        GetComponent<MeshRenderer>().material = red;
    }
}
