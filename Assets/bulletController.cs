using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletController : MonoBehaviour
{
    public float bulletSpeed;
    public float eBulletSpeed;

    string sceneName;

    GameObject world;

    bool timeStop;

    void Start(){
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "new"){
            world = GameObject.Find("World");
        }
        else{
            world = null;
        }
    }

    void FixedUpdate()
    {
        if (world != null){
            timeStop = world.GetComponent<worldController>().timeStop;
        }
        else{
            timeStop = false;
        }
        if (timeStop == false){
            if (this.tag == "Bullet") {
                transform.Translate(Vector3.forward * bulletSpeed);
            }
            if (this.tag == "Enemy bullet") {
                transform.Translate(Vector3.forward * eBulletSpeed);
            }
            if ((transform.position.x > 52 || transform.position.x < -52 || transform.position.z > 26 || transform.position.z < -26) && sceneName == "new")
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnBecomeInvisible(){
        Destroy(this.gameObject);
    }
}
