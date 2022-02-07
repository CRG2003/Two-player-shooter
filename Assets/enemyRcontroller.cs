using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRcontroller : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip shoot;

    GameObject player1;
    GameObject player2;
    GameObject player;
    GameObject world;

    public GameObject EBullet;
    public GameObject powerUp;

    public float enemySpeed;
    public float enemyFireRate;
    float shotTimer;

    float health = 3; 
    float switchTimer;
    bool playerSwitch;

    public Material flash;
    public Material red;

    Light thisLight;

    void Start()
    {
        thisLight = GetComponentInChildren<Light>();

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player = player1;

        world = GameObject.Find("World");

        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (player1 != null && world.GetComponent<worldController>().timeStop == false){
            player = player1.GetComponent<PlayerController>().player;
            switchTimer -= Time.deltaTime;
            shotTimer -= Time.deltaTime;

            transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
            if (shotTimer <= 0){
                Instantiate(EBullet, transform.position, transform.rotation);
                shotTimer = enemyFireRate;
                audio.PlayOneShot(shoot);
            }

            if (Vector3.Distance(player.transform.position, transform.position) < 20){
                transform.Translate(-Vector3.forward * enemySpeed);
            }
            else{
                transform.Translate(Vector3.forward * enemySpeed);
            }
        }
        if (world.GetComponent<worldController>().stage != "Survive"){
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision info)
    {
        if (info.collider.tag == "Bullet")
        {
            StartCoroutine(Flash());

            health -= 1;
            transform.Translate(-Vector3.forward * enemySpeed * 20f);
            if (health == 2)
            {
                thisLight.intensity = 10;
            }
            if (health == 1)
            {
                thisLight.intensity = 5;
            }
            if (health <= 0)
            {
                if (Random.Range(0f, 10f) > 9f){
                    Instantiate(powerUp, transform.position, transform.rotation);
                }
                Destroy(this.gameObject);
            }
            Destroy(info.collider.gameObject);
        }
    }
    IEnumerator Flash()
    {
        thisLight.color = Color.white;
        GetComponent<MeshRenderer>().material = flash;
        yield return new WaitForSeconds(0.08f);
        thisLight.color = Color.red;
        GetComponent<MeshRenderer>().material = red;
    }
}