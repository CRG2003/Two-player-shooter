using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float eBulletSpeed;

    void FixedUpdate()
    {
        if (this.tag == "Bullet") {
            transform.Translate(Vector3.forward * bulletSpeed);
        }
        if (this.tag == "Enemy bullet") {
            transform.Translate(Vector3.forward * eBulletSpeed);
        }
        if (transform.position.x > 52 || transform.position.x < -52 || transform.position.z > 26 || transform.position.z < -26)
        {
            Destroy(this.gameObject);
        }
    }
}
