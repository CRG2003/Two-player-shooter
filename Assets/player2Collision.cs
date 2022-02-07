using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Collision : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip hit;

    public Material selected;
    public Material nSelected;
    public Material flash;

    public GameObject player;

    public Light pLight;
    bool godMode;

    void Start(){
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision info)
    {
        godMode = player.GetComponent<PlayerController>().godMode;
        if (info.collider.tag == "Enemy")
        {
            if (godMode == false){
                player.GetComponent<PlayerController>().health -= 1;
            }
            audio.PlayOneShot(hit);
            StartCoroutine(Flash());
            Vector3 dir = info.transform.position - transform.position;
            info.collider.GetComponent<Rigidbody>().AddForce(dir * 25, ForceMode.Impulse);
        }
        if (info.collider.tag == "Enemy bullet")
        {
            if (godMode == false){
                player.GetComponent<PlayerController>().health -= 1;
            }
            audio.PlayOneShot(hit);
            info.transform.Translate(-Vector3.forward * 20000000000000000f);
        }
        if (info.collider.tag == "Power up"){
           if (info.gameObject.GetComponent<powerUpController>().type == true){
               player.GetComponent<PlayerController>().playerSpeed *= 1.5f;
           } 
           else{
               player.GetComponent<PlayerController>().damageUp = true;
           }
           player.GetComponent<PlayerController>().powerUpTimer = 15;
           Destroy(info.gameObject);
        }
    }
    IEnumerator Flash()
    {
        pLight.GetComponent<Light>().color = Color.white;
        GetComponent<MeshRenderer>().material = flash;
        yield return new WaitForSeconds(0.05f);
        pLight.GetComponent<Light>().color = Color.yellow;
        GetComponent<MeshRenderer>().material = selected;
    }
}
