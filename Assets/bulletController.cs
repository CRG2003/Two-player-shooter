using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float bulletSpeed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed);
        if (transform.position.x > 52 || transform.position.x < -52 || transform.position.z > 26 || transform.position.z < -26)
        {
            Destroy(this.gameObject);
        }
    }
}
