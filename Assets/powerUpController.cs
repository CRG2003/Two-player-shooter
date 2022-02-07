using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpController : MonoBehaviour
{
    public bool type;

    public Material speed;
    public Material damage;

    void Start()
    {
        if (Random.Range(-1f, 1f) < 0)
        {
            type = true;
            GetComponent<MeshRenderer>().material = speed;
        }
        else
        {
            type = false;
            GetComponent<MeshRenderer>().material = damage;
        }
    }

    void Update()
    {
        transform.Rotate(0, transform.rotation.y + 2, 0);
    }
}
