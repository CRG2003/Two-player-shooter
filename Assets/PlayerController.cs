using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float fireRate;
    public float health = 5;

    float rayLen;
    float switchTimer;
    float shotTimer;

    public Material selected;
    public Material nSelected;
    public Material flash;

    public GameObject player2;
    public GameObject p1Light;
    public GameObject p2Light;
    public GameObject bullet;
    private GameObject b1;
    private GameObject b2;
    private GameObject b3;

    public bool firing;
    public bool playerSwitch = false;

    private Camera mainCamera;

    Rigidbody p1b;
    Rigidbody p2b;

    Vector3 dir;

    void Start()
    {
        p1b = GetComponent<Rigidbody>();
        p2b = player2.GetComponent<Rigidbody>();
        p2Light.GetComponent<Light>().enabled = false;

        mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        if (this != null)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(cameraRay, out rayLen))
            {
                if (playerSwitch == false)
                {
                    Vector3 pointToLook = cameraRay.GetPoint(rayLen);
                    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                }
                else
                {
                    Vector3 pointToLook = cameraRay.GetPoint(rayLen);
                    player2.transform.LookAt(new Vector3(pointToLook.x, player2.transform.position.y, pointToLook.z));
                }
            }
            if (Input.GetKey(KeyCode.Mouse0) && shotTimer <= 0)
            {
                if (playerSwitch == false)
                {
                    Instantiate(bullet, transform.position, transform.rotation);
                    shotTimer = fireRate;
                }
                else
                {
                    Instantiate(bullet, player2.transform.position, player2.transform.rotation);
                    shotTimer = fireRate;
                }
            }
            if (Input.GetKey(KeyCode.Mouse1) && shotTimer <= 0)
            {
                if (playerSwitch == false)
                {
                    b1 = Instantiate(bullet, transform.position, transform.rotation);
                    b1.transform.Rotate(0, 7, 0);
                    b2 = Instantiate(bullet, transform.position, transform.rotation);
                    b3 = Instantiate(bullet, transform.position, transform.rotation);
                    b3.transform.Rotate(0, -8, 0);
                    shotTimer = fireRate * 3;
                }
                else
                {
                    b1 = Instantiate(bullet, player2.transform.position, player2.transform.rotation);
                    b1.transform.Rotate(0, 7, 0);
                    b2 = Instantiate(bullet, player2.transform.position, player2.transform.rotation);
                    b3 = Instantiate(bullet, player2.transform.position, player2.transform.rotation);
                    b3.transform.Rotate(0, -8, 0);
                    shotTimer = fireRate * 3;
                }
            }

            switchTimer -= Time.deltaTime;
            shotTimer -= Time.deltaTime;

            if (playerSwitch == false)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    p1b.AddForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    p1b.AddForce(new Vector3(0, 0, -1) * playerSpeed, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    p1b.AddForce(new Vector3(-1, 0, 0) * playerSpeed, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    p1b.AddForce(new Vector3(1, 0, 0) * playerSpeed, ForceMode.Impulse);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    p2b.AddForce(new Vector3(0, 0, 1) * playerSpeed, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    p2b.AddForce(new Vector3(0, 0, -1) * playerSpeed, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    p2b.AddForce(new Vector3(-1, 0, 0) * playerSpeed, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    p2b.AddForce(new Vector3(1, 0, 0) * playerSpeed, ForceMode.Impulse);
                }
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (playerSwitch == true && switchTimer <= 0)
                {
                    playerSwitch = false;
                    switchTimer = 1;
                    GetComponent<MeshRenderer>().material = selected;
                    p1Light.GetComponent<Light>().enabled = true;
                    player2.GetComponent<MeshRenderer>().material = nSelected;
                    p2Light.GetComponent<Light>().enabled = false;
                }
                else if (switchTimer <= 0)
                {
                    playerSwitch = true;
                    switchTimer = 1;
                    GetComponent<MeshRenderer>().material = nSelected;
                    p1Light.GetComponent<Light>().enabled = false;
                    player2.GetComponent<MeshRenderer>().material = selected;
                    p2Light.GetComponent<Light>().enabled = true;
                }
            }
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
            health -= 1;
            StartCoroutine(Flash());
            info.transform.Translate(-Vector3.forward * 20f);
        }
        if (info.collider.tag == "Enemy bullet")
        {
            health -= 1;
            info.transform.Translate(-Vector3.forward * 2000000000000000f);
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