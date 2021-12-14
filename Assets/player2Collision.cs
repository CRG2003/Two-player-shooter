using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Collision : MonoBehaviour
{
    public Material selected;
    public Material nSelected;
    public Material flash;

    public GameObject player;

    public Light pLight;

    void OnCollisionEnter(Collision info)
    {
        if (info.collider.tag == "Enemy")
        {
            player.GetComponent<PlayerController>().health -= 1;
            StartCoroutine(Flash());
            Vector3 dir = info.transform.position - transform.position;
            info.collider.GetComponent<Rigidbody>().AddForce(dir * 25, ForceMode.Impulse);
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
