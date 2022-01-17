using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpController : MonoBehaviour
{
    public GameObject player1;

    float switchTimer;
    float powerUpTime;


    void Update()
    {
        switchTimer -= Time.deltaTime;
        powerUpTime -= Time.deltaTime;

        if (powerUpTime <= 0f)
        {
            Debug.Log(powerUpTime);
            player1.GetComponent<PlayerController>().playerSpeed = 4;
        }
        else
        {
            Debug.Log("Fast Speed");
        }

        transform.Rotate(0, transform.rotation.y + 10, 0);
    }

    void OnCollisionEnter(Collision info)
    {
        if (info.collider.tag == "Player")
        {
            player1.GetComponent<PlayerController>().playerSpeed = 10;
            powerUpTime = 10;

            Destroy(this.gameObject);
        }
    }
}
