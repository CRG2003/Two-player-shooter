using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject bullet;
    public float enemySpeed;

    float health = 3;
    float switchTimer;
    bool playerSwitch;

    public Material flash;
    public Material red;

    Light thisLight;

    void Start()
    {
        thisLight = GetComponentInChildren<Light>();
    }

    void FixedUpdate()
    {
        playerSwitch = player1.GetComponent<PlayerController>().getSwitch();
        switchTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
        {
            if (playerSwitch == true && switchTimer <= 0)
            {
                playerSwitch = false;
                switchTimer = 1;
            }
            else if (switchTimer <= 0)
            {
                playerSwitch = true;
                switchTimer = 1;
            }
        }
        if (playerSwitch == false)
        {
            transform.LookAt(new Vector3(player1.transform.position.x, 1, player1.transform.position.z));
        }
        else if (playerSwitch == true)
        {
            transform.LookAt(new Vector3(player2.transform.position.x, 1, player2.transform.position.z));
        }
        transform.Translate(Vector3.forward * enemySpeed);

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
