using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public GameObject EBullet;

    public float speed;
    public float health;
    public float attackSpeed;

    GameObject b;
    GameObject player;
    GameObject player1;
    GameObject world;

    float attackTimer = 0;
    float moveTimer;
    float switchTimer;
    float s = 0;
    float direction;
    string attackType = "spread";

    bool animFreeze;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 360);
        transform.Rotate(0, direction, 0);

        world = GameObject.Find("World");
        player1 = GameObject.Find("Player1");
        player = player1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animFreeze = world.GetComponent<worldController>().animFreeze;
        player = player1.GetComponent<PlayerController>().player;
        if (player1 != null && world.GetComponent<worldController>().timeStop == false && animFreeze == false){
            if (health <= 66 && attackType == "spread"){
                attackType = "burst";
            }
            if (health <= 33 && attackType == "burst"){
                attackType = "berserk";
                speed *= 1.2f;
            }

            attackTimer -= Time.deltaTime;
            moveTimer -= Time.deltaTime;
            switchTimer -= Time.deltaTime;

            if (attackTimer <= 0){
                if (attackType == "spread"){
                    if (s == 0){
                        s = 22.5f;
                    }
                    else{
                        s = 0;
                    }
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 45+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 90+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 135+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 180+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 225+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 270+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 315+s, 0);

                    attackTimer = attackSpeed;
                }
                if (attackType == "burst"){
                    StartCoroutine(burstAttack());  
                    attackTimer = attackSpeed;        
                }
                if (attackType == "berserk"){
                    if (s == 0){
                        s = 22.5f;
                    }
                    else{
                        s = 0;
                    }
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 45+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 90+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 135+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 180+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 225+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 270+s, 0);
                    b = Instantiate(EBullet, transform.position, transform.rotation);
                    b.transform.Rotate(0, 315+s, 0);    

                    StartCoroutine(burstAttack());   
                    attackTimer = attackSpeed;            
                }
            }

            if (moveTimer <= 0){
                direction = Random.Range(0, 360);
                moveTimer = 10;
                transform.rotation = Quaternion.Euler(0, direction, 0);
            }

            transform.Translate(Vector3.forward * speed);
            if (transform.position.z > 18.6f  || transform.position.z < -18.6f){
                direction = 180 - direction;
                transform.rotation = Quaternion.Euler(0, direction, 0);
            }
            if (transform.position.x > 44 || transform.position.x < -44){
                direction = -direction;
                transform.rotation = Quaternion.Euler(0, direction, 0);
            }
        }
    }
    IEnumerator burstAttack(){
        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b.transform.Rotate(0, 5, 0); 
        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b.transform.Rotate(0, -5, 0);  
        yield return new WaitForSeconds(0.2f);

        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b.transform.Rotate(0, 3f, 0); 
        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b.transform.Rotate(0, -3f, 0); 
        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b.transform.Rotate(0, 9f, 0); 
        b = Instantiate(EBullet, transform.position, transform.rotation);
        b.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
        b.transform.Rotate(0, -9f, 0);    
        yield return new WaitForSeconds(0.8f);      
    }

    void OnCollisionEnter(Collision info){
        if (info.collider.tag == "Bullet"){
            health -= 1;
            if (health <= 0){
                Destroy(this.gameObject);
            }
            Destroy(info.collider.gameObject);
        }
    }
}
