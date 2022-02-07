using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip shoot1;
    public AudioClip shoot2;
    public AudioClip shoot3;
    private AudioClip shoot;
    public AudioClip hit;

    public float playerSpeed = 4;
    float fireRate = 0.4f;
    public float health = 5;

    float rayLen;
    float switchTimer;
    float shotTimer;
    
    public float powerUpTimer;

    public Material selected;
    public Material nSelected;
    public Material flash;

    GameObject player2;
    GameObject world;

    public GameObject p1Light;
    public GameObject p2Light;
    public GameObject bullet;
    private GameObject b1;
    private GameObject b2;
    private GameObject b3;
    public GameObject player;

    public bool playerSwitch = false;
    public bool damageUp = false;
    public bool powerUp = false;
    public bool animFreeze;
    public bool godMode = false;
    float godTimer = 0;

    private Camera mainCamera;

    Rigidbody pb;

    Vector3 dir;

    void Start()
    {
        player = this.gameObject;
        pb = GetComponent<Rigidbody>();
        p2Light.GetComponent<Light>().color = new Vector4 (1, 1, 1, 0.3f);

        mainCamera = FindObjectOfType<Camera>();

        player2 = GameObject.Find("Player2");
        world = GameObject.Find("World");

        audio = GetComponent<AudioSource>();

    }

    void FixedUpdate()
    {
        animFreeze = world.GetComponent<worldController>().animFreeze;
        if (this != null && world.GetComponent<worldController>().stage != "victory" && animFreeze != true){
            // Timers  
            switchTimer -= Time.deltaTime;
            shotTimer -= Time.deltaTime;
            powerUpTimer -= Time.deltaTime;
            godTimer -= Time.deltaTime;


            // PLayer switch 
            if (Input.GetKey(KeyCode.E) && switchTimer <= 0){
                if (player == player2){
                    player = this.gameObject;
                    pb = GetComponent<Rigidbody>();

                    GetComponent<MeshRenderer>().material = selected;
                    player2.GetComponent<MeshRenderer>().material = nSelected;
                    p2Light.GetComponent<Light>().color = new Vector4 (1, 1, 1, 0.3f);
                    p1Light.GetComponent<Light>().color = new Vector4(1, 0.94f, 0, 1);
                }

                else{
                    player = player2;
                    pb = player2.GetComponent<Rigidbody>();

                    GetComponent<MeshRenderer>().material = nSelected;
                    player2.GetComponent<MeshRenderer>().material = selected;
                    p1Light.GetComponent<Light>().color = new Vector4(1, 1, 1, 0.3f);
                    p2Light.GetComponent<Light>().color = new Vector4(1, 0.94f, 0, 1);
                }
                switchTimer = 0.5f;
            }       
            if (Input.GetKey(KeyCode.Q) && godTimer <= 0){
                if (godMode == true){
                    godMode = false;
                }
                else{
                    godMode = true;
                }
                godTimer = 0.2f;
            }

            // Player face camera      
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(cameraRay, out rayLen))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLen);
                player.transform.LookAt(new Vector3(pointToLook.x, player.transform.position.y, pointToLook.z));

            }

            if (Random.Range(0f, 3f) <= 1f){
                shoot = shoot1;
            }
            else if (Random.Range(0f, 2f) <= 1f){
                shoot = shoot2;
            }
            else{
                shoot = shoot3;
            }

            // Standered shot
            if (Input.GetKey(KeyCode.Mouse0) && shotTimer <= 0)
            {
                if (damageUp)
                {
                    Instantiate(bullet, player.transform.position, player.transform.rotation);
                    Instantiate(bullet, player.transform.position, player.transform.rotation);
                    audio.PlayOneShot(shoot);
                    shotTimer = fireRate;
                }
                else
                {
                    Instantiate(bullet, player.transform.position, player.transform.rotation);
                    audio.PlayOneShot(shoot);
                    shotTimer = fireRate;
                }
            }

            // Secondary shot 
            if (Input.GetKey(KeyCode.Mouse1) && shotTimer <= 0)
            {
                b1 = Instantiate(bullet, player.transform.position, player.transform.rotation);
                b1.transform.Rotate(0, 7, 0);
                b2 = Instantiate(bullet, player.transform.position, player.transform.rotation);
                b3 = Instantiate(bullet, player.transform.position, player.transform.rotation);
                b3.transform.Rotate(0, -8, 0);
                audio.PlayOneShot(shoot);
                audio.PlayOneShot(shoot);
                shotTimer = fireRate * 2;  
            }

            // Power up handling 
            if (powerUpTimer <= 0)
            {
                playerSpeed = 4;
                damageUp = false;
                powerUp = false;
            }

            // Player movement 
            if (Input.GetKey(KeyCode.W))
            {
                pb.AddForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.S))
            {
                pb.AddForce(new Vector3(0, 0, -1) * playerSpeed, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.A))
            {
                pb.AddForce(new Vector3(-1, 0, 0) * playerSpeed, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.D))
            {
                pb.AddForce(new Vector3(1, 0, 0) * playerSpeed, ForceMode.Impulse);
            }

            // Handles death 
            if (health <= 0)
            {
                Destroy(this.gameObject);
                Destroy(player2.gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision info)
    {
        if (info.collider.tag == "Enemy")
        {
            if (godMode == false){
                health -= 1;
            }
            audio.PlayOneShot(hit);
            StartCoroutine(Flash());
            info.transform.Translate(-Vector3.forward * 20f);
        }
        if (info.collider.tag == "Enemy bullet")
        {
            if (godMode == false){
                health -= 1;
            }
            audio.PlayOneShot(hit);
            info.transform.Translate(-Vector3.forward * 2000000000000000f);
        }
        if (info.collider.tag == "Power up" && powerUp == false)
        {
            powerUp = true;
            if (info.gameObject.GetComponent<powerUpController>().type == true)
            {
                playerSpeed *= 1.5f;
            }
            else
            {
                damageUp = true;
            }
            powerUpTimer = 15;
            world.GetComponent<worldController>().powerUpTimer = 15.0f;
            Destroy(info.gameObject);
        }
    }
    IEnumerator Flash()
    {
        p1Light.GetComponent<Light>().color = Color.white;
        GetComponent<MeshRenderer>().material = flash;
        yield return new WaitForSeconds(0.05f);
        p1Light.GetComponent<Light>().color = Color.yellow;
        GetComponent<MeshRenderer>().material = selected;
    }
    public bool getSwitch()
    {
        return playerSwitch;
    }
}